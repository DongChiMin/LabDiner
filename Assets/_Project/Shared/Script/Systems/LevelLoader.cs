using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LabDiner.Shared
{
    public class LevelLoader : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] DishDatabaseSO globalDatabase;
        [SerializeField] LevelConfigSO currentLevelConfig;

        // Danh sách các món "có thể ra lò" ở level hiện tại
        [Header("[Debug] Runtime Data")]
        [SerializeField] private List<DishSO> discoverableDishes = new List<DishSO>();

        public void Init()
        {
            discoverableDishes.Clear();

            // Lấy danh sách ID nguyên liệu có sẵn trong level để so sánh cho nhanh
            var availableIngredientIds = new HashSet<string>(
                currentLevelConfig.availableItems.Select(x => x.id)
            );

            foreach (var dish in globalDatabase.allDishes)
            {
                // Món ăn khả thi là món mà TẤT CẢ nguyên liệu của nó đều nằm trong danh sách Available
                bool canMake = dish.recipe.All(ing => availableIngredientIds.Contains(ing.id));

                if (canMake)
                {
                    discoverableDishes.Add(dish);
                }
            }
        }


        // Các service public để các hệ thống khác có thể truy cập
        public List<DishSO> GetDiscoverableDishes() => discoverableDishes;
        public List<IngredientSO> GetAvailableIngredients() => currentLevelConfig.availableItems;

    }
}
