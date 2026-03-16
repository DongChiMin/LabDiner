using UnityEngine;

namespace LabDiner.Shared
{
    public class PoolMember : MonoBehaviour, IPoolable 
{
    private SceneObjectPooling _originPool;

    public void SetOrigin(SceneObjectPooling pool) => _originPool = pool;

    public void ReturnToPool() 
    {
        if (_originPool != null) 
        {
            _originPool.ReturnToPool(gameObject);
        }
        else 
        {
            Destroy(gameObject); // Fallback nếu có lỗi
        }
    }
}
}