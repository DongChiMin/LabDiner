using System.Collections.Generic;
using UnityEngine;

namespace LabDiner.Shared
{
    [CreateAssetMenu(fileName = "New Dish", menuName = "Cooking/Dish")]
    public class DishSO : ScriptableObject
    {
        public string id;
        public string dishName;
        public List<IngredientSO> recipe; // Danh sách nguyên liệu
        public Sprite icon;

        // Thuộc tính để Scale
        public float basePrice;
        public FlavorTag flavor; // Dùng để check "Khách mong đợi"
    }
}