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
                OnIngredientRemoved?.Raise(ingredient.IngredientData);

                if(ingredient.TryGetComponent(out PoolMember poolMember))
                {
                    poolMember.ReturnToPool();
                }
                else
                {
                    Debug.LogWarning($"LabIngredient {ingredient.name} không có PoolMember để trả về pool!");
                    Destroy(ingredient.gameObject); // Fallback nếu có lỗi
                }
            }
        }
    }
}
