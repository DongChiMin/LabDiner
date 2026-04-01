using System.Collections;
using UnityEngine;

namespace LabDiner.Restaurant
{
    public class ChefBehavior : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float cookMultiplier = 1f;
        [SerializeField] private float placeOnPassTableDuration = 0f;

        private ChefContext _context;

        void Start()
        {
            _context = GetComponent<ChefContext>();    
        }

        public IEnumerator Cook(CookingTask task)
        {
            Debug.Log("TODO: hoàn thiện công thức thời gian nấu");
            float cookTime = 3 * (1/ cookMultiplier);
            _context.ProgressPieLogic.StartProgressPie(cookTime);
            yield return new WaitForSeconds(cookTime);
            task.StationTarget.SetStatus(true);
            _context.CarryDishLogic.UpdateCookingTaskPrice(task);
            _context.CarryDishLogic.CarryDish(task);
        }

        public IEnumerator PlaceOnPassTable(CookingTask task)
        {
            task.PassTableTarget.PlaceTaskOnPassTable(task);
            // Di chuyển món ăn đến PassTable, bật hiệu ứng đặt món...
            _context.ProgressPieLogic.StartProgressPie(placeOnPassTableDuration);
            yield return new WaitForSeconds(placeOnPassTableDuration);
            _context.CarryDishLogic.Finish(task);
        }
    }
}