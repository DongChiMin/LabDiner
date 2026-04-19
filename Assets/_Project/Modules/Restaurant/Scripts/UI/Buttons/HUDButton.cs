using LabDiner.Shared.Event;
using UnityEngine;
using UnityEngine.UI;

namespace LabDiner.Restaurant.UI
{
    public class HUDButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private UIPopupEvent _onPopupShow;

        void Awake()
        {
            _button.onClick.AddListener(() => {
                _onPopupShow.Raise();
            });
        }
    }
}
