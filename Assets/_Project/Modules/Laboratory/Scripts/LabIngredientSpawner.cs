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
            if (_isDisableWhenClick)
            {
                ToggleSpawner(false);
            }

            // 1. Spawn item từ pool
            var go = PoolManager.Instance.IngredientPool.Get(worldPosition + Vector3.down * 0.5f, Quaternion.identity);

            //2. Kiểm tra xem item mới spawn có SpawnerMember chưa, nếu chưa thì add vào để sau này trả về spawner
            if (!go.TryGetComponent(out SpawnerMember spawnerMember))
            {
                // Nếu không có thì add thêm và log ra console
                spawnerMember = go.AddComponent<SpawnerMember>();
                Debug.Log($"<color=yellow>[PoolManager]</color> Đã thêm SpawnerMember vào {go.name} vì prefab đang thiếu!");
            }
            spawnerMember.SetOrigin(this);

            // 3. Lấy component Draggable từ item mới
            if (go.TryGetComponent(out IDraggable newDraggable))
            {
                return newDraggable;
            }

            return null;
        }

        public void ToggleSpawner(bool isEnabled)
        {
            Debug.Log("TODO: làm hiệu ứng xuất hiện + biến mất cho spawner");
            if (_collider != null)
                _collider.enabled = isEnabled;

            if (_visual != null)
                _visual.SetActive(isEnabled);
        }
    }
}
