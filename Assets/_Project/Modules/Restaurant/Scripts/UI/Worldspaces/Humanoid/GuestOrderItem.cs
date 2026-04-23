using System.Collections.Generic;
using LabDiner.Restaurant.Environment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LabDiner.Restaurant.UI
{
    public class GuestOrderItem : MonoBehaviour
    {
        [SerializeField] private Image _dishIcon;
        [SerializeField] private TextMeshProUGUI _quantityText;

        public void Setup(Sprite dishSprite, int quantity)
        {
            _dishIcon.sprite = dishSprite;
            _quantityText.text = quantity.ToString();
        }

        public void DecreaseQuantity()
        {
            if (int.TryParse(_quantityText.text, out int currentQuantity))
            {
                currentQuantity = Mathf.Max(0, currentQuantity - 1);
                _quantityText.text = currentQuantity.ToString();
            }
            else
            {
                Debug.LogWarning("Failed to parse quantity text.");
            }
        }
    }
}
