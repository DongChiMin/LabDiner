
using LabDiner.Restaurant.Event;
using UnityEngine;
using UnityEngine.UI;

namespace LabDiner.Restaurant.UI
{
    public class ReputationBarHUD : MonoBehaviour
    {
        [SerializeField] private FloatEvent _onReputationChanged;
        [SerializeField] private Image _fillImage;
        [Header("[DEBUG]")]
        [SerializeField] private float _currentReputationRatio;

        void OnEnable()
        {
            _onReputationChanged.Register(UpdateReputation);
        }

        void OnDisable()
        {
            _onReputationChanged.Unregister(UpdateReputation);
        }

        public void UpdateReputation(float ratio)
        {
            _fillImage.fillAmount = ratio;
        }
    }
}
        