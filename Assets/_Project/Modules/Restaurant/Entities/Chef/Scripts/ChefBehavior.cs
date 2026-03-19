using System.Collections;
using UnityEngine;

namespace LabDiner.Restaurant
{
    public class ChefBehavior : MonoBehaviour
    {
        public IEnumerator Cook(float duration)
        {
            Debug.Log("Đang nấu món...");
            // Bật hiệu ứng khói, lửa, âm thanh xèo xèo...
            yield return new WaitForSeconds(duration);
            Debug.Log("Nấu xong!");
        }

        public void PickUpIngredient() => Debug.Log("Lấy nguyên liệu");
        public void PlateFood() => Debug.Log("Trình bày món ăn");
    }
}