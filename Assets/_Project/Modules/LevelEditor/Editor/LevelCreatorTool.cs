using UnityEngine;
using UnityEditor;
using System.IO;
using LabDiner.Restaurant.SO; 
using LabDiner.LevelSystem.Domain; // Namespace chứa LevelRegistrySO của bạn

namespace LabDiner.Restaurant.Editor
{
    public class LevelCreatorTool : EditorWindow
    {
        private int _levelIndex = 1;
        
        private const string TemplateFolderPath = "Assets/_Project/Modules/Restaurant/Data/Levels/_Example";
        private const string RootDestinationPath = "Assets/_Project/Modules/Restaurant/Data/Levels";
        
        // Đường dẫn cố định tới file Registry theo yêu cầu của bạn
        private const string RegistryAssetPath = "Assets/_Project/Modules/Restaurant/Data/Levels/_Registry/LevelRegistry.asset";

        [MenuItem("LabDiner/🚀 Level Creator Tool")]
        public static void ShowWindow()
        {
            var window = GetWindow<LevelCreatorTool>("Level Creator Tool");
            window.minSize = new Vector2(350, 200);
            window.maxSize = new Vector2(350, 200);
        }

        private void OnGUI()
        {
            GUILayout.Space(15);
            EditorGUILayout.LabelField("🏭 KHỞI TẠO TIẾN TRÌNH LEVEL", EditorStyles.boldLabel);
            GUILayout.Space(10);

            _levelIndex = EditorGUILayout.IntField("Số Thứ Tự Level Index:", _levelIndex);

            if (_levelIndex < 1)
            {
                EditorGUILayout.HelpBox("Level Index phải lớn hơn hoặc bằng 1!", MessageType.Error);
                GUI.enabled = false;
            }

            GUILayout.FlexibleSpace();

            GUI.backgroundColor = new Color(0.2f, 0.6f, 1f);
            if (GUILayout.Button("✨ KHỞI TẠO LEVEL", GUILayout.Height(40)))
            {
                ExecuteScaffolding();
            }
            GUI.backgroundColor = Color.white;
            GUI.enabled = true;
            GUILayout.Space(15);
        }

        private void ExecuteScaffolding()
        {
            if (!AssetDatabase.IsValidFolder(TemplateFolderPath))
            {
                EditorUtility.DisplayDialog("Lỗi Hệ Thống", $"Không tìm thấy thư mục mẫu tại đường dẫn:\n{TemplateFolderPath}", "Kiểm tra lại");
                return;
            }

            string newFolderName = $"level_{_levelIndex}";
            string destinationPath = $"{RootDestinationPath}/{newFolderName}";

            if (AssetDatabase.IsValidFolder(destinationPath))
            {
                bool overwrite = EditorUtility.DisplayDialog("Cảnh báo", $"Thư mục [{newFolderName}] đã tồn tại! Bạn có chắc muốn ghi đè?", "Có, ghi đè", "Không");
                if (!overwrite) return;
                
                AssetDatabase.DeleteAsset(destinationPath);
            }

            if (!AssetDatabase.CopyAsset(TemplateFolderPath, destinationPath))
            {
                Debug.LogError($"[Scaffolder] Thất bại khi copy từ {TemplateFolderPath} sang {destinationPath}");
                return;
            }

            AssetDatabase.Refresh();

            string prefabPath = "";
            string configPath = "";

            string[] allAssetPaths = AssetDatabase.FindAssets("", new string[] { destinationPath });
            System.Collections.Generic.List<(string oldPath, string newName)> assetsToRename = new System.Collections.Generic.List<(string, string)>();

            foreach (string guid in allAssetPaths)
            {
                string currentPath = AssetDatabase.GUIDToAssetPath(guid);
                string fileName = Path.GetFileNameWithoutExtension(currentPath);
                string extension = Path.GetExtension(currentPath);

                if (extension == ".prefab" && currentPath.Contains(destinationPath) && !currentPath.Contains("/Mission") && !currentPath.Contains("/Upgrade"))
                {
                    assetsToRename.Add((currentPath, $"Level {_levelIndex}"));
                    prefabPath = $"{Path.GetDirectoryName(currentPath)}/Level {_levelIndex}{extension}";
                }
                else if (extension == ".asset" && currentPath.Contains(destinationPath) && !currentPath.Contains("/Mission") && !currentPath.Contains("/Upgrade"))
                {
                    assetsToRename.Add((currentPath, $"Level {_levelIndex}"));
                    configPath = $"{Path.GetDirectoryName(currentPath)}/Level {_levelIndex}{extension}";
                }
            }

            foreach (var asset in assetsToRename)
            {
                AssetDatabase.RenameAsset(asset.oldPath, asset.newName);
            }
            AssetDatabase.Refresh();

            CreatePrefabVariant(prefabPath);
            
            // Nạp file Config vừa tạo lên bộ nhớ để tiến hành liên kết dữ liệu
            LevelConfigSO configSO = AssetDatabase.LoadAssetAtPath<LevelConfigSO>(configPath);
            LinkPrefabToConfig(configSO, prefabPath);

            // 🔥 BƯỚC MỚI: Tự động đăng ký file Config này vào file Registry tổng
            RegisterToLevelRegistry(configSO);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Object createdFolder = AssetDatabase.LoadAssetAtPath<Object>(destinationPath);
            EditorGUIUtility.PingObject(createdFolder);
            
            EditorUtility.DisplayDialog("Thành công!", $"Đã cấu hình xong cây thư mục và đăng ký Level {_levelIndex}!", "Tuyệt vời");
        }

        private void CreatePrefabVariant(string pathOfCopiedPrefab)
        {
            string[] sourcePrefabGUIDs = AssetDatabase.FindAssets("t:Prefab", new string[] { TemplateFolderPath });
            if (sourcePrefabGUIDs.Length == 0) return;

            string sourcePrefabPath = AssetDatabase.GUIDToAssetPath(sourcePrefabGUIDs[0]);
            GameObject sourcePrefabObj = AssetDatabase.LoadAssetAtPath<GameObject>(sourcePrefabPath);

            if (sourcePrefabObj != null)
            {
                GameObject tempInstance = (GameObject)PrefabUtility.InstantiatePrefab(sourcePrefabObj);
                if (tempInstance != null)
                {
                    PrefabUtility.SaveAsPrefabAsset(tempInstance, pathOfCopiedPrefab);
                    DestroyImmediate(tempInstance);
                }
            }
        }

        private void LinkPrefabToConfig(LevelConfigSO configSO, string prefabPath)
        {
            GameObject prefabObj = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (configSO != null)
            {
                SerializedObject serializedConfig = new SerializedObject(configSO);
                
                SerializedProperty prefabProperty = serializedConfig.FindProperty("LevelPrefab");
                if (prefabProperty != null && prefabObj != null)
                {
                    prefabProperty.objectReferenceValue = prefabObj;
                }

                SerializedProperty indexProperty = serializedConfig.FindProperty("LevelIndex");
                if (indexProperty != null)
                {
                    indexProperty.intValue = _levelIndex;
                }

                serializedConfig.ApplyModifiedProperties();
                EditorUtility.SetDirty(configSO);
            }
        }

        private void RegisterToLevelRegistry(LevelConfigSO newConfigSO)
        {
            if (newConfigSO == null) return;

            // 1. Tải file Registry tổng lên bộ nhớ
            LevelRegistrySO registrySO = AssetDatabase.LoadAssetAtPath<LevelRegistrySO>(RegistryAssetPath);

            if (registrySO == null)
            {
                Debug.LogError($"[Scaffolder] Không tìm thấy file LevelRegistry tại đường dẫn: {RegistryAssetPath}. Vui lòng tạo file trước!");
                return;
            }

            // Dùng SerializedObject để chỉnh sửa List 'registry' chuẩn Editor, hỗ trợ quản lý bộ nhớ tốt hơn
            SerializedObject serializedRegistry = new SerializedObject(registrySO);
            SerializedProperty registryProperty = serializedRegistry.FindProperty("registry");

            if (registryProperty != null && registryProperty.isArray)
            {
                int existingIndexInList = -1;

                // 2. VÒNG QUÉT DỌN SẠCH (CLEAN UP): Duyệt ngược từ cuối danh sách lên đầu
                for (int i = registryProperty.arraySize - 1; i >= 0; i--)
                {
                    SerializedProperty elementProp = registryProperty.GetArrayElementAtIndex(i);
                    LevelConfigSO elementAsset = elementProp.objectReferenceValue as LevelConfigSO;

                    // Trường hợp A: File cũ bị Designer xóa mất (Bị Null/Missing trong List) -> Xóa luôn phần tử rác này
                    if (elementAsset == null)
                    {
                        registryProperty.DeleteArrayElementAtIndex(i);
                        continue;
                    }

                    // Trường hợp B: Tìm thấy phần tử trùng LevelIndex (Có thể trùng do làm lại hoặc ghi đè)
                    if (elementAsset.LevelIndex == _levelIndex)
                    {
                        existingIndexInList = i;
                    }
                }

                // 3. THAY THẾ HOẶC THÊM MỚI THÔNG MINH
                if (existingIndexInList != -1)
                {
                    // Nếu trùng LevelIndex, gán đè trực tiếp file Config mới vào đúng vị trí (Slot) đó
                    SerializedProperty targetSlot = registryProperty.GetArrayElementAtIndex(existingIndexInList);
                    targetSlot.objectReferenceValue = newConfigSO;
                    Debug.Log($"<color=orange>[Registry]</color> Đã phát hiện trùng ID! Tiến hành thay thế file Config mới vào vị trí Level {_levelIndex} trong Registry.");
                }
                else
                {
                    // Nếu là một Level hoàn toàn mới, tăng size mảng và nhét vào cuối danh sách
                    int newSlotIndex = registryProperty.arraySize;
                    registryProperty.InsertArrayElementAtIndex(newSlotIndex);
                    registryProperty.GetArrayElementAtIndex(newSlotIndex).objectReferenceValue = newConfigSO;
                    Debug.Log($"<color=lime>[Registry]</color> Đã tự động đăng ký file Config Level {_levelIndex} vào danh sách Registry.");
                }

                // Thực thi lưu thay đổi ngầm vào file Registry cứng
                serializedRegistry.ApplyModifiedProperties();
                EditorUtility.SetDirty(registrySO);
            }
            else
            {
                Debug.LogError("[Scaffolder] Không tìm thấy biến List tên là 'registry' trong file LevelRegistrySO!");
            }
        }
    }
}