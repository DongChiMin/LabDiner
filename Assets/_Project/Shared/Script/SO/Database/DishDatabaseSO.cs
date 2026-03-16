using System.Collections.Generic;
using UnityEngine;

namespace LabDiner.Shared
{
    [CreateAssetMenu(fileName = "Global Dish Database", menuName = "Cooking/Global Dish Database")]
    public class DishDatabaseSO : ScriptableObject
    {
        public List<DishSO> allDishes; // Kéo tất cả 1000 món vào đây một lần duy nhất
    }
}
