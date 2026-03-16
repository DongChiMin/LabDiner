using System.Collections;
using System.Collections.Generic;
using LabDiner.Shared;
using UnityEngine;

public class SceneObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialSize = 10;
    [SerializeField] private int objectsPerFrame = 1; // Số lượng object tạo tối đa mỗi frame

    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    void Start()
    {
        // Thay vì dùng vòng lặp for, ta chạy Coroutine để khởi tạo dần dần
        StartCoroutine(SetupPoolGradually());
    }

    private IEnumerator SetupPoolGradually()
    {
        int createdCount = 0;

        while (createdCount < initialSize)
        {
            // Tính toán số lượng cần tạo trong frame này
            int batchSize = Mathf.Min(objectsPerFrame, initialSize - createdCount);

            for (int i = 0; i < batchSize; i++)
            {
                CreateNewObject();
                createdCount++;
            }

            // Đợi đến frame tiếp theo mới tạo tiếp
            yield return null;
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, transform);

        if (!obj.TryGetComponent<PoolMember>(out PoolMember poolMember))
        {
            // Nếu không có thì add thêm và log ra console
            poolMember = obj.AddComponent<PoolMember>();
            Debug.Log($"<color=yellow>[PoolManager]</color> Đã thêm PoolMember vào {obj.name} vì prefab đang thiếu!");
        }
        poolMember.SetOrigin(this);

        obj.SetActive(false);
        poolQueue.Enqueue(obj);
        return obj;
    }

    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        // Nếu game cần dùng ngay khi pool chưa khởi tạo xong (Lazy Loading)
        GameObject obj = (poolQueue.Count > 0) ? poolQueue.Dequeue() : CreateNewObject();

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        // Kiểm tra tránh enqueue trùng lặp nếu object đã nằm trong pool
        if (obj.activeSelf)
        {
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }
}