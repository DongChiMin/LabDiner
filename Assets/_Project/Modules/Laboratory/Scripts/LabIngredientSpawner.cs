using LabDiner.Shared;
using UnityEngine;

namespace LabDiner.Laboratory
{
    public class LabIngredientSpawner : MonoBehaviour, IClickable
    {
        [SerializeField] bool _isDisableWhenClick;
        [SerializeField] Collider2D _collider;
        [SerializeField] GameObject _visual;
        public void OnPointerUp(Vector3 worldPosition)
        {

        }

        public IDraggable OnPointerDown(Vector3 worldPosition)
        {
            // if (_isDisableWhenClick)
            // {
            //     ToggleSpawner(false);
            // }

            // 1. Spawn item từ pool
            var go = PoolManager.Instance.IngredientPool.Get(worldPosition + Vector3.down * 0.5f, Quaternion.identity);

            // 2. Lấy component Draggable từ item mới
            if (go.TryGetComponent(out IDraggable newDraggable))
            {
                return newDraggable;
            }

            return null;
        }

        private void ToggleSpawner(bool isEnabled)
        {
            if (_collider != null)
                _collider.enabled = isEnabled;

            if (_visual != null)
                _visual.SetActive(isEnabled);
        }
    }
}
