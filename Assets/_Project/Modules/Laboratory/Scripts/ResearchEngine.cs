using System.Collections.Generic;
using System.Linq;
using LabDiner.Shared;
using UnityEngine;

namespace LabDiner.Laboratory
{
    public static class ResearchEngine
    {
        public static DishSO GetResult(List<IngredientSO> selectedIngredients, List<DishSO> database)
        {
            if (selectedIngredients == null || selectedIngredients.Count == 0)
            {
                Debug.LogWarning("Chưa chọn nguyên liệu.");
                return null;
            }

            // Sắp xếp ID nguyên liệu đầu vào để so sánh không quan trọng thứ tự
            var inputIds = selectedIngredients.Select(x => x.id).OrderBy(id => id).ToList();

            foreach (var dish in database)
            {
                // Lấy công thức của món ăn và sắp xếp tương tự
                var recipeIds = dish.recipe.Select(x => x.id).OrderBy(id => id).ToList();

                // So sánh 2 danh sách
                if (inputIds.SequenceEqual(recipeIds))
                {
                    return dish;
                }
            }

            Debug.LogWarning("Không tìm thấy món ăn phù hợp với các nguyên liệu đã chọn.");
            return null; // Không tìm thấy món nào khớp
        }
    }
}
