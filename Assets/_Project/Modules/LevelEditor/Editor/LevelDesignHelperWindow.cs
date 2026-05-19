using UnityEngine;

namespace LabDiner.LevelEditor
{
    using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace LabDiner.LevelSystem.Editor
{
    public class LevelDesignHelperWindow : EditorWindow
    {
        private Vector2 _scrollPosition;

        [MenuItem("LabDiner/🖲️ Level Design Helper")]
        public static void ShowWindow()
        {
            var window = GetWindow<LevelDesignHelperWindow>("Level Helper");
            window.minSize = new Vector2(300, 400);
        }

        private void OnGUI()
        {
            // Kiểm tra xem Designer có đang mở Prefab Mode hay không
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (prefabStage == null)
            {
                EditorGUILayout.HelpBox("Vui lòng click đúp mở một Prefab Level (Prefab Mode) để sử dụng công cụ này!", MessageType.Info);
                return;
            }

            // Hiển thị tên Prefab hiện tại đang chỉnh sửa
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label($"🎯 Prefab đang mở: {prefabStage.prefabContentsRoot.name}", EditorStyles.boldLabel);
            EditorGUILayout.EndVertical();

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            GUILayout.Space(10);

            // ================== KHU VỰC PHÂN LOẠI HIGHLIGHT ==================
            
            DrawHighlightSection("Nhân Viên (Staff System)", "Rest Position", "RestPos", Color.cyan);
            DrawHighlightSection("Khu Vực Bếp (Cooking System)", "Core Station", "CoreStation", Color.orange);
            DrawHighlightSection("Khách Hàng (Guest System)", "Vùng Vui Chơi / Ăn uống", "GuestArea", Color.green);
            DrawHighlightSection("Hệ Thống Đường Đi", "NavMesh Modifier Vùng", "NavMeshModifier", Color.magenta);

            // ================================================================

            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// Hàm phụ trách vẽ một cụm chức năng highlight theo từ khóa
        /// </summary>
        private void DrawHighlightSection(string sectionName, string displayName, string searchKeyword, Color highlightColor)
        {
            GUILayout.Label(sectionName, EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            
            // Nút 1: Tìm và Chọn (Ping & Select) trong Hierarchy
            if (GUILayout.Button($"🔍 Tìm {displayName}", GUILayout.Height(28)))
            {
                FindAndSelectObjects(searchKeyword);
            }

            // Nút 2: Kích hoạt nháy màu để nhận diện trực quan trên Scene View
            GUI.backgroundColor = highlightColor;
            if (GUILayout.Button("✨ Flash Highlight", GUILayout.Width(110), GUILayout.Height(28)))
            {
                FlashObjectsInScene(searchKeyword);
            }
            GUI.backgroundColor = Color.white; // Reset màu button về mặc định

            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();
            GUILayout.Space(8);
        }

        private void FindAndSelectObjects(string keyword)
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage == null) return;

            // Quét toàn bộ Transform con bên trong Prefab đang mở
            Transform[] allChildren = prefabStage.prefabContentsRoot.GetComponentsInChildren<Transform>(true);
            var targetObjects = new System.Collections.Generic.List<GameObject>();

            foreach (var child in allChildren)
            {
                // Tìm kiếm dựa theo: Tên Object chứa từ khóa HOẶC Component chứa từ khóa đó
                if (child.name.ToLower().Contains(keyword.ToLower()) || HasComponentWithKeyword(child.gameObject, keyword))
                {
                    targetObjects.Add(child.gameObject);
                }
            }

            if (targetObjects.Count > 0)
            {
                // Ép Unity chọn (bôi xanh) các Object này trong bảng Hierarchy
                Selection.objects = targetObjects.ToArray();
                
                // Ping cái đầu tiên để bảng Hierarchy tự động cuộn đến vị trí của nó
                EditorGUIUtility.PingObject(targetObjects[0]);
                Debug.Log($"<color=lime>[Tool]</color> Đã tìm thấy và chọn {targetObjects.Count} object chứa từ khóa [{keyword}].");
            }
            else
            {
                Debug.LogWarning($"<color=yellow>[Tool]</color> Không tìm thấy object nào khớp với từ khóa [{keyword}] trong Prefab này.");
            }
        }

        private void FlashObjectsInScene(string keyword)
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage == null) return;

            Transform[] allChildren = prefabStage.prefabContentsRoot.GetComponentsInChildren<Transform>(true);
            
            foreach (var child in allChildren)
            {
                if (child.name.ToLower().Contains(keyword.ToLower()) || HasComponentWithKeyword(child.gameObject, keyword))
                {
                    // Sử dụng công cụ ngầm EditorGUIUtility.PingObject để làm cái Object đó nhấp nháy phát sáng trên Scene View
                    EditorGUIUtility.PingObject(child.gameObject);
                }
            }
            
            // Ép Scene View vẽ lại ngay lập tức để tạo hiệu ứng thị giác
            SceneView.RepaintAll();
        }

        private bool HasComponentWithKeyword(GameObject go, string keyword)
        {
            Component[] components = go.GetComponents<Component>();
            foreach (var comp in components)
            {
                if (comp == null) continue;
                if (comp.GetType().Name.ToLower().Contains(keyword.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
}
