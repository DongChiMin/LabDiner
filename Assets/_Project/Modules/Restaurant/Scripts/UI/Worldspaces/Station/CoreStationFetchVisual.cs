using LabDiner.Restaurant.Environment;
using LabDiner.Shared.Event;
using LabDiner.Shared.Input;
using LabDiner.Shared.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LabDiner.Restaurant.UI
{
    public class CoreStationFetchVisual : MonoBehaviour
    {
        [Header("[Edit Mode Only]")]
        [SerializeField] private SpriteRenderer _buttonIcon;
        [SerializeField] private Image _unlockStationIcon;
        [SerializeField] private TextMeshProUGUI _upgradeStationText;
        [SerializeField] private TextMeshProUGUI _unlockStationName;

        public void FetchVisual(CoreStationUIData data)
        {
#if UNITY_EDITOR
            // Hủy các lệnh gọi delay cũ trước đó (nếu có) để tránh bị spam gọi trùng khi bạn gõ phím nhanh trên Inspector
            UnityEditor.EditorApplication.delayCall -= () => ExecuteFetch(data);

            // Đăng ký lệnh gọi mới
            UnityEditor.EditorApplication.delayCall += () =>
            {
                // Kiểm tra null đề phòng trường hợp bạn vừa xóa Object trong lúc Editor đang delay
                if (this != null)
                {
                    ExecuteFetch(data);
                }
            };
#endif

        }

        private void ExecuteFetch(CoreStationUIData data)
        {

            _buttonIcon.sprite = data.CoreStationSO.Dish.DishIcon;
            _unlockStationIcon.sprite = data.CoreStationSO.Dish.StationIcon;
            _unlockStationName.text = data.CoreStationSO.Dish.Name;
            _upgradeStationText.text = data.CoreStationSO.Dish.Name;
        }
    }
}