using System.Collections.Generic;
using UnityEngine;

namespace LabDiner.Shared
{
    [CreateAssetMenu(fileName = "New Ingredient", menuName = "Cooking/Ingredient")]
    public class IngredientSO : ScriptableObject
    {
        [Header("Ingredient Details")]
        public string id;          // Dùng để so sánh công thức (ví dụ: "tomato", "bread")
        public string itemName;    // Tên hiển thị (ví dụ: "Fresh Tomato")
        public Sprite icon;        // Hình ảnh hiển thị trong Lab và Shop
    }
}