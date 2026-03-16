
using LabDiner.Shared;
using UnityEngine;

namespace LabDiner.Laboratory
{
    public class Cauldron : MonoBehaviour
    {

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IIngredient>(out var ingredient))
            {
                // 2. Lấy Rigidbody2D của vật đang rơi để kiểm tra vận tốc
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    // 3. Chỉ tính nếu vận tốc Y < -0.1f (đang rơi xuống)
                    if (rb.linearVelocity.y < -0.1f)
                    {
                        var data = ingredient.IngredientData;
                        Debug.Log($"Phát hiện nguyên liệu {data.name} rơi từ trên xuống!");
                    }
                }
            }
        }
    }
}