using LabDiner.Shared;
using LabDiner.Shared.Events;
using UnityEngine;

namespace LabDiner.Laboratory
{
    public class LabIngredientRemover : MonoBehaviour
    {
        [SerializeField] private IngredientEvent OnIngredientRemoved;
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out LabIngredient ingredient))
            {
                //1. Phát hiện nguyên liệu rơi vào vùng xóa
                OnIngredientRemoved?.Raise(ingredient.IngredientData);

                //2. Trả nguyên liệu về pool thay vì destroy
                ReturnToPool(ingredient);

                //3. Gọi hàm OnEndDrag trên SpawnerMember nếu tồn tại
                ReturnToSpawner(ingredient);
            }
        }

        void ReturnToPool(LabIngredient ingredient)
        {
            if (ingredient.TryGetComponent(out PoolMember poolMember))
            {
                poolMember.ReturnToPool();
            }
            else
            {
                Debug.LogWarning($"LabIngredient {ingredient.name} không có PoolMember để trả về pool!");
                Destroy(ingredient.gameObject); // Fallback nếu có lỗi
            }
        }

        void ReturnToSpawner(LabIngredient ingredient)
        {
            if (ingredient.TryGetComponent(out SpawnerMember spawnerMember))
            {
                spawnerMember.ReturnToSpawner();
            }
            else
            {
                Debug.LogWarning($"LabIngredient {ingredient.name} không có SpawnerMember để trả về spawner!");
            }
        }
    }
}
