// using UnityEngine;

// namespace LabDiner.LevelEditor
// {
//     using UnityEditor;
//     using LabDiner.Restaurant.SO;
//     using LabDiner.Restaurant.Enum; // Đảm bảo namespace này chứa StaffType
//     using LabDiner.Restaurant.Event; // Chứa các ScriptableEvent của bạn
//     using LabDiner.Restaurant.Workflow;

//     public class LevelUpgradeFactoryWindow : EditorWindow
//     {
//         private enum UpgradeCategory { Guest, Staff, Station }
//         private UpgradeCategory _currentCategory = UpgradeCategory.Guest;

//         // Thư mục đích để lưu Asset (Designer kéo thả thư mục hoặc chọn qua nút)
//         private DefaultAsset _targetFolder;
//         private string _fileName = "New_Upgrade_Card";

//         // --- Dữ liệu dùng chung (BaseUpgradeSO) ---
//         private string _title;
//         private string _description;
//         private Sprite _icon;
//         private Sprite _upgradeTypeSprite;
//         private double _upgradeCost;
//         private float _upgradeValue;

//         // --- Dữ liệu đặc thù theo từng loại ---
//         private GuestUpgradeType _guestType;
//         private GuestUpgradeEvent _guestEvent;

//         private StaffUpgradeType _staffType;
//         private StaffType _staffTarget;
//         private StaffUpgradeEvent _staffEvent;

//         private StationUpgradeType _stationType;
//         private DishSO _targetDish;
//         private StationUpgradeEvent _stationEvent;

//         [MenuItem("LabDiner/Tools/2. Level Upgrade Creator")]
//         public static void ShowWindow()
//         {
//             var window = GetWindow<LevelUpgradeFactoryWindow>("Level Upgrade Creator");
//             window.minSize = new Vector2(450, 600);
//         }

//         private void OnGUI()
//         {
//             GUILayout.Space(10);
//             DrawFolderSelector();

//             GUILayout.Space(10);
//             // 1. THANH PHÂN CHIA LOẠI UPGRADE (TABS)
//             _currentCategory = (UpgradeCategory)GUILayout.Toolbar((int)_currentCategory, new string[] { "👥 Guest Upgrade", "🏃 Staff Upgrade", "🍳 Station Upgrade" }, GUILayout.Height(30));

//             GUILayout.Space(10);
//             EditorGUILayout.BeginVertical("box");
//             GUILayout.Label("📝 THÔNG TIN CHUNG (BASE DATA)", EditorStyles.boldLabel);
//             GUILayout.Space(5);

//             _fileName = EditorGUILayout.TextField("Tên File Asset:", _fileName);
//             _title = EditorGUILayout.TextField("Tiêu Đề (Title):", _title);

//             EditorGUILayout.LabelField("Mô Tả (Description):");
//             _description = EditorGUILayout.TextArea(_description, GUILayout.Height(50));

//             _icon = (Sprite)EditorGUILayout.ObjectField("Icon Thẻ:", _icon, typeof(Sprite), false);
//             _upgradeTypeSprite = (Sprite)EditorGUILayout.ObjectField("Upgrade Type Sprite:", _upgradeTypeSprite, typeof(Sprite), false);
//             _upgradeCost = EditorGUILayout.DoubleField("Giá Nâng Cấp (Cost):", _upgradeCost);
//             _upgradeValue = EditorGUILayout.FloatField("Chỉ Số Cộng Thêm (Value):", _upgradeValue);

//             EditorGUILayout.EndVertical();

//             GUILayout.Space(10);

//             // 2. FORM ĐẶC THÙ THEO TỪNG TAB
//             EditorGUILayout.BeginVertical("box");
//             GUILayout.Label("⚙️ THÔNG SỐ ĐẶC THÙ (SPECIFIC DATA)", EditorStyles.boldLabel);
//             GUILayout.Space(5);

//             switch (_currentCategory)
//             {
//                 case UpgradeCategory.Guest:
//                     DrawGuestForm();
//                     break;
//                 case UpgradeCategory.Staff:
//                     DrawStaffForm();
//                     break;
//                 case UpgradeCategory.Station:
//                     DrawStationForm();
//                     break;
//             }

//             EditorGUILayout.EndVertical();

//             // 3. NÚT XUẤT ASSET
//             GUILayout.FlexibleSpace();
//             DrawCreateButton();
//             GUILayout.Space(10);
//         }

//         private void DrawFolderSelector()
//         {
//             EditorGUILayout.BeginVertical("box");
//             GUILayout.Label("📁 THƯ MỤC LƯU ASSET FILE", EditorStyles.miniBoldLabel);
//             EditorGUILayout.BeginHorizontal();

//             _targetFolder = (DefaultAsset)EditorGUILayout.ObjectField("Thư mục đích:", _targetFolder, typeof(DefaultAsset), false);

//             if (GUILayout.Button("Chọn Thư Mục", GUILayout.Width(110)))
//             {
//                 string path = EditorUtility.OpenFolderPanel("Chọn thư mục lưu Card nâng cấp", "Assets", "");
//                 if (!string.IsNullOrEmpty(path))
//                 {
//                     string relativePath = "Assets" + path.Substring(Application.dataPath.Length);
//                     _targetFolder = AssetDatabase.LoadAssetAtPath<DefaultAsset>(relativePath);
//                 }
//             }
//             EditorGUILayout.EndHorizontal();
//             EditorGUILayout.EndVertical();
//         }

//         private void DrawGuestForm()
//         {
//             _guestType = (GuestUpgradeType)EditorGUILayout.EnumPopup("Loại Nâng Cấp Khách:", _guestType);
//             _guestEvent = (GuestUpgradeEvent)EditorGUILayout.ObjectField("Event Hệ Thống (SO):", _guestEvent, typeof(GuestUpgradeEvent), false);
//         }

//         private void DrawStaffForm()
//         {
//             _staffType = (StaffUpgradeType)EditorGUILayout.EnumPopup("Loại Nâng Cấp NV:", _staffType);
//             _staffTarget = (StaffType)EditorGUILayout.EnumPopup("Đối Tượng Áp Dụng:", _staffTarget);
//             _staffEvent = (StaffUpgradeEvent)EditorGUILayout.ObjectField("Event Hệ Thống (SO):", _staffEvent, typeof(StaffUpgradeEvent), false);
//         }

//         private void DrawStationForm()
//         {
//             _stationType = (StationUpgradeType)EditorGUILayout.EnumPopup("Loại Nâng Cấp Bếp:", _stationType);
//             _targetDish = (DishSO)EditorGUILayout.ObjectField("Món Ăn Target (Để trống = Toàn bộ):", _targetDish, typeof(DishSO), false);
//             _stationEvent = (StationUpgradeEvent)EditorGUILayout.ObjectField("Event Hệ Thống (SO):", _stationEvent, typeof(StationUpgradeEvent), false);
//         }

//         private void DrawCreateButton()
//         {
//             if (_targetFolder == null)
//             {
//                 GUI.enabled = false; // Khóa nút nếu chưa chọn thư mục
//             }

//             GUI.backgroundColor = new Color(0.2f, 0.8f, 0.4f);
//             if (GUILayout.Button("✨ HOÀN THÀNH & TẠO CARD UPGRADE", GUILayout.Height(35)))
//             {
//                 CreateUpgradeAsset();
//             }
//             GUI.backgroundColor = Color.white;
//             GUI.enabled = true;
//         }

//         private void CreateUpgradeAsset()
//         {
//             string folderPath = AssetDatabase.GetAssetPath(_targetFolder);
//             string fullAssetPath = $"{folderPath}/{_fileName}.asset";

//             // Kiểm tra trùng tên file tránh đè dữ liệu cũ
//             fullAssetPath = AssetDatabase.GenerateUniqueAssetPath(fullAssetPath);

//             BaseUpgradeSO newUpgrade = null;

//             // Khởi tạo đúng Class dựa theo Tab đang chọn
//             switch (_currentCategory)
//             {
//                 case UpgradeCategory.Guest:
//                     var guestSO = CreateInstance<GuestUpgradeSO>();
//                     guestSO.UpgradeType = _guestType;
//                     guestSO.OnUpgradeRaised = _guestEvent;
//                     newUpgrade = guestSO;
//                     break;

//                 case UpgradeCategory.Staff:
//                     var staffSO = CreateInstance<StaffUpgradeSO>();
//                     staffSO.UpgradeType = _staffType;
//                     staffSO.Target = _staffTarget;
//                     staffSO.OnUpgradeRaised = _staffEvent;
//                     newUpgrade = staffSO;
//                     break;

//                 case UpgradeCategory.Station:
//                     var stationSO = CreateInstance<StationUpgradeSO>();
//                     stationSO.UpgradeType = _stationType;
//                     stationSO.TargetDish = _targetDish;
//                     stationSO.OnUpgradeRaised = _stationEvent;
//                     newUpgrade = stationSO;
//                     break;
//             }

//             if (newUpgrade != null)
//             {
//                 // Gán dữ liệu chung (Base Class)
//                 newUpgrade.Title = _title;
//                 newUpgrade.Description = _description;
//                 newUpgrade.Icon = _icon;
//                 newUpgrade.UpgradeTypeSprite = _upgradeTypeSprite;
//                 newUpgrade.UpgradeCost = _upgradeCost;
//                 newUpgrade.UpgradeValue = _upgradeValue;

//                 // Lưu thực thể thành file cứng (.asset) trong Project
//                 AssetDatabase.CreateAsset(newUpgrade, fullAssetPath);
//                 AssetDatabase.SaveAssets();

//                 // Ping trực tiếp tới file vừa tạo để Designer kiểm tra lại
//                 EditorGUIUtility.PingObject(newUpgrade);
//                 Debug.Log($"<color=lime>[Factory]</color> Tạo thành công Card nâng cấp tại: {fullAssetPath}");

//                 // Reset bớt các trường thông tin tên để sẵn sàng tạo thẻ tiếp theo
//                 _fileName = "New_Upgrade_Card";
//                 _title = "";
//                 _description = "";
//             }
//         }
//     }

// }
