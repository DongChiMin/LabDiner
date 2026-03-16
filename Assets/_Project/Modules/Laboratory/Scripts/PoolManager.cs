using UnityEngine;

namespace LabDiner.Laboratory
{
    public class PoolManager : Singleton<PoolManager>
    {
        [Header("Pool References")]
        [SerializeField] private SceneObjectPooling _ingredientPool;

        // Các script khác chỉ có thể đọc, không thể gán lại
        public SceneObjectPooling IngredientPool => _ingredientPool;
    }
}