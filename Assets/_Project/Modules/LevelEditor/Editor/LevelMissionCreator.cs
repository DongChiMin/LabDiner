// using UnityEngine;
// using UnityEditor;
// using LabDiner.Restaurant.SO;
// using LabDiner.Shared.Event; // Đảm bảo namespace này chứa các định nghĩa liên quan

// namespace LabDiner.Restaurant.Editor
// {
//     public class MissionFactoryWindow : EditorWindow
//     {
//         private enum MissionCategory { CoreStation, FinalLevel, Upgrade }
//         private MissionCategory _currentCategory = MissionCategory.CoreStation;

//         private DefaultAsset _targetFolder;
//         private string _fileName = "New_Mission_Card";

//         // --- Dữ liệu Base (BaseMissionSO) ---
//         private string _title;
//         private Sprite _missionIcon;
//         private float _targetValue = 1f; // Mặc định là 1 phòng hờ cho FinalLevel
//         private BaseRewardSO _reward;
//         private double _rewardValue;

//         // --- Dữ liệu Đặc thù từng loại ---
//         // 1. CoreStation Mission
//         private CoreStationSO _targetCoreStation;
//         private CoreStationMissionType _coreStationMissionType;
//         private CoreStationRuntimeSO _coreStationRuntimeSO;

//         // 2. FinalLevel Mission
//         private FinalMissionType _finalMissionType = FinalMissionType.AllCoreStationLevel;
//         private CoreStationRuntimeSO _finalLevelRuntimeSO;

//         // 3. Upgrade Mission
//         private BaseUpgradeSO _targetUpgrade;
//         private LevelUpgradeRuntimeSO _levelUpgradeRuntimeSO;

//         [MenuItem("LabDiner/Tools/3. Level Mission Creator")]
//         public static void ShowWindow()
//         {
//             var window = GetWindow<MissionFactoryWindow>("Level Mission Creator");
//             window.minSize = new Vector2(460, 650);
//         }

//         private void OnGUI()
//         {
//             GUILayout.Space(10);
//             DrawFolderSelector();

//             GUILayout.Space(10);
//             // 1. THANH TABS CHỌN LOẠI NHIỆM VỤ
//             _currentCategory = (MissionCategory)GUILayout.Toolbar((int)_currentCategory, 
//                 new string[] { "🏪 CoreStation", "🏆 Final Level", "📈 Upgrade" }, GUILayout.Height(30));

//             GUILayout.Space(10);
            
//             // 2. THÔNG TIN CHUNG (BASE DATA)
//             EditorGUILayout.BeginVertical("box");
//             GUILayout.Label("📝 THÔNG TIN CHUNG (BASE MISSION DATA)", EditorStyles.boldLabel);
//             GUILayout.Space(5);

//             _fileName = EditorGUILayout.TextField("Tên File Asset:", _fileName);
//             _title = EditorGUILayout.TextField("Tiêu Đề Nhiệm Vụ:", _title);
//             _missionIcon = (Sprite)EditorGUILayout.ObjectField("Mission Icon:", _missionIcon, typeof(Sprite), false);
            
//             // Nếu là Final Level thì khóa hiển thị Target Value là 1 (100%) để Designer khỏi sửa nhầm
//             if (_currentCategory == MissionCategory.FinalLevel)
//             {
//                 GUI.enabled = false;
//                 EditorGUILayout.FloatField("Giá Trị Đích (Target Value):", 1f);
//                 _targetValue = 1f;
//                 GUI.enabled = true;
//             }
//             else
//             {
//                 _targetValue = EditorGUILayout.FloatField("Giá Trị Đích (Target Value):", _targetValue);
//             }

//             GUILayout.Space(5);
//             GUILayout.Label("🎁 Phần Thưởng (Reward)", EditorStyles.miniBoldLabel);
//             _reward = (BaseRewardSO)EditorGUILayout.ObjectField("Loại Phần Thưởng (SO):", _reward, typeof(BaseRewardSO), false);
//             _rewardValue = EditorGUILayout.DoubleField("Giá Trị Thưởng:", _rewardValue);

//             EditorGUILayout.EndVertical();

//             GUILayout.Space(10);

//             // 3. THÔNG SỐ ĐẶC THÙ (SPECIFIC DATA)
//             EditorGUILayout.BeginVertical("box");
//             GUILayout.Label("⚙️ CẤU HÌNH LOGIC ĐẶC THÙ", EditorStyles.boldLabel);
//             GUILayout.Space(5);

//             switch (_currentCategory)
//             {
//                 case MissionCategory.CoreStation:
//                     DrawCoreStationForm();
//                     break;
//                 case MissionCategory.FinalLevel:
//                     DrawFinalLevelForm();
//                     break;
//                 case MissionCategory.Upgrade:
//                     DrawUpgradeForm();
//                     break;
//             }

//             EditorGUILayout.EndVertical();

//             // 4. NÚT TẠO FILE ASSET
//             GUILayout.FlexibleSpace();
//             DrawCreateButton();
//             GUILayout.Space(10);
//         }

//         private void DrawFolderSelector()
//         {
//             EditorGUILayout.BeginVertical("box");
//             GUILayout.Label("📁 THƯ MỤC LƯU FILE MISSION", EditorStyles.miniBoldLabel);
//             EditorGUILayout.BeginHorizontal();
//             _targetFolder = (DefaultAsset)EditorGUILayout.ObjectField("Thư mục lưu:", _targetFolder, typeof(DefaultAsset), false);
//             if (GUILayout.Button("Chọn Thư Mục", GUILayout.Width(110)))
//             {
//                 string path = EditorUtility.OpenFolderPanel("Chọn thư mục lưu Mission", "Assets", "");
//                 if (!string.IsNullOrEmpty(path))
//                 {
//                     string relativePath = "Assets" + path.Substring(Application.dataPath.Length);
//                     _targetFolder = AssetDatabase.LoadAssetAtPath<DefaultAsset>(relativePath);
//                 }
//             }
//             EditorGUILayout.EndHorizontal();
//             EditorGUILayout.EndVertical();
//         }

//         private void DrawCoreStationForm()
//         {
//             _coreStationMissionType = (CoreStationMissionType)EditorGUILayout.EnumPopup("Loại Nhiệm Vụ Trạm:", _coreStationMissionType);
//             _targetCoreStation = (CoreStationSO)EditorGUILayout.ObjectField("Trạm Mục Tiêu (Target):", _targetCoreStation, typeof(CoreStationSO), false);
//             _coreStationRuntimeSO = (CoreStationRuntimeSO)EditorGUILayout.ObjectField("CoreStation Runtime (Static):", _coreStationRuntimeSO, typeof(CoreStationRuntimeSO), false);
//         }

//         private void DrawFinalLevelForm()
//         {
//             _finalMissionType = (FinalMissionType)EditorGUILayout.EnumPopup("Loại Nhiệm Vụ Cuối:", _finalMissionType);
//             _finalLevelRuntimeSO = (CoreStationRuntimeSO)EditorGUILayout.ObjectField("CoreStation Runtime (Static):", _finalLevelRuntimeSO, typeof(CoreStationRuntimeSO), false);
//             EditorGUILayout.HelpBox("Nhiệm vụ này tự động tính tiến độ theo tỷ lệ % tổng level trạm đạt được.", MessageType.Info);
//         }

//         private void DrawUpgradeForm()
//         {
//             _targetUpgrade = (BaseUpgradeSO)EditorGUILayout.ObjectField("Thẻ Cần Mua (Để trống = Bất kỳ):", _targetUpgrade, typeof(BaseUpgradeSO), false);
//             _levelUpgradeRuntimeSO = (LevelUpgradeRuntimeSO)EditorGUILayout.ObjectField("Level Upgrade Runtime (Static):", _levelUpgradeRuntimeSO, typeof(LevelUpgradeRuntimeSO), false);
//         }

//         private void DrawCreateButton()
//         {
//             if (_targetFolder == null) GUI.enabled = false;

//             GUI.backgroundColor = new Color(0.2f, 0.7f, 1f); // Màu xanh dương highlight cho Mission
//             if (GUILayout.Button("✨ HOÀN THÀNH & TẠO FILE MISSION", GUILayout.Height(35)))
//             {
//                 CreateMissionAsset();
//             }
//             GUI.backgroundColor = Color.white;
//             GUI.enabled = true;
//         }

//         private void CreateMissionAsset()
//         {
//             string folderPath = AssetDatabase.GetAssetPath(_targetFolder);
//             string fullAssetPath = $"{folderPath}/{_fileName}.asset";
//             fullAssetPath = AssetDatabase.GenerateUniqueAssetPath(fullAssetPath);

//             BaseMissionSO newMission = null;

//             switch (_currentCategory)
//             {
//                 case MissionCategory.CoreStation:
//                     var coreStationObj = CreateInstance<CoreStationMissionSO>();
//                     coreStationObj.MissionType = _coreStationMissionType;
//                     coreStationObj.TargetCoreStation = _targetCoreStation;
//                     coreStationObj.coreStationRuntimeSO = _coreStationRuntimeSO;
//                     newMission = coreStationObj;
//                     break;

//                 case MissionCategory.FinalLevel:
//                     var finalLevelObj = CreateInstance<FinalLevelMissionSO>();
//                     finalLevelObj.MissionType = _finalMissionType;
//                     // Vì biến nội bộ trong class của bạn đặt là _coreStationRuntimeSO (private serialize)
//                     // Chúng ta sử dụng Reflection hoặc gán qua SerializedObject nếu bị chặn, nhưng ở đây gán trực tiếp nếu đổi thành public/internal
//                     // Giả sử bạn chỉnh trường này thành public hoặc có setter để Tool gán được:
//                     SetPrivateRuntimeSO(finalLevelObj, _finalLevelRuntimeSO);
//                     newMission = finalLevelObj;
//                     break;

//                 case MissionCategory.Upgrade:
//                     var upgradeObj = CreateInstance<UpgradeMissionSO>();
//                     upgradeObj.TargetUpgrade = _targetUpgrade;
//                     upgradeObj.levelUpgradeRuntimeSO = _levelUpgradeRuntimeSO;
//                     newMission = upgradeObj;
//                     break;
//             }

//             if (newMission != null)
//             {
//                 // Gán dữ liệu BaseClass
//                 newMission.Title = _title;
//                 newMission.MissionIcon = _missionIcon;
//                 newMission.TargetValue = _targetValue;
//                 newMission.Reward = _reward;
//                 newMission.RewardValue = _rewardValue;

//                 AssetDatabase.CreateAsset(newMission, fullAssetPath);
//                 AssetDatabase.SaveAssets();

//                 EditorGUIUtility.PingObject(newMission);
//                 Debug.Log($"<color=lime>[Factory]</color> Đã sinh File Mission thành công tại: {fullAssetPath}");

//                 // Reset trường tên để chuẩn bị cho thẻ tiếp theo
//                 _fileName = "New_Mission_Card";
//                 _title = "";
//             }
//         }

//         // Mẹo dùng SerializedObject để gán dữ liệu cho biến Private [_coreStationRuntimeSO] của FinalLevelMissionSO mà không cần sửa code gốc
//         private void SetPrivateRuntimeSO(FinalLevelMissionSO targetSO, CoreStationRuntimeSO runtimeSO)
//         {
//             SerializedObject so = new SerializedObject(targetSO);
//             SerializedProperty prop = so.FindProperty("_coreStationRuntimeSO");
//             if (prop != null)
//             {
//                 prop.objectReferenceValue = runtimeSO;
//                 so.ApplyModifiedProperties();
//             }
//         }
//     }
// }