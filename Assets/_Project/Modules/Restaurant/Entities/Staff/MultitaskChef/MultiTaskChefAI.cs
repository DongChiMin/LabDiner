
using System.Collections;
using UnityEngine;

namespace LabDiner.Restaurant
{
    public class MultitaskChefAI : MonoBehaviour
    {
        [Header("Event")]
        [SerializeField] private MultitaskChefEvent _onMultitaskChefAvailable;
        [Header("[DEBUG]")]
        [SerializeField] private CookingTask currentTask;

        private StaffMover _mover;
        private MultitaskChefBehavior _behavior;
        private MultitaskChefContext _context;
        [Header("[DEBUG]")]
        [SerializeField] private Order servingOrder;

        void Start()
        {
            _mover = GetComponent<StaffMover>();
            _behavior = GetComponent<MultitaskChefBehavior>();
            _context = GetComponent<MultitaskChefContext>();
        }

        private IEnumerator DoCookAndShip(CookingTask task)
        {
            currentTask = task;
            Station station = task.StationTarget;
            PassTable passTable = task.PassTableTarget;


            //1. Di chuyển đến vị trí của Station
            yield return _mover.MoveTo(station.WorkPos.position);

            //2. Nấu
            yield return _behavior.Cook(task);

            //3. Di chuyển đến vị trí khách
            yield return _mover.MoveTo(task.Order.OrderBy.DiningTable.WorkPos.position);

            //4. Đưa món cho khách
            yield return _context.CtxBehavior.GiveFoodToGuest(task);

            //5. Quay về vị trí nghỉ ngơi hoặc làm task khác nếu có
            yield return Rest();
        }

        IEnumerator DoServe(Order order)
        {
            servingOrder = order;
            // 1. Đi đến bàn
            yield return _context.CtxMover.MoveTo(servingOrder.OrderBy.DiningTable.WorkPos.position);

            //2. Phục vụ
            yield return _context.CtxBehavior.Serve(servingOrder);

            //3. Thông báo hoàn thành

            //3. Quay về vị trí ban đầu (hoặc có thể đi phục vụ bàn khác nếu có order mới)
            yield return Rest(order);
        }

        public void StartCookAndShip(CookingTask task)
        {
            StopAllCoroutines();
            StartCoroutine(DoCookAndShip(task));
        }

        public void StartServe(Order order)
        {
            StopAllCoroutines();
            StartCoroutine(DoServe(order));
        }

        IEnumerator Rest()
        {
            currentTask = null;
            _onMultitaskChefAvailable.Raise(_context);
            yield return _mover.MoveTo(_context.RestPosition.position);
        }

        IEnumerator Rest(IStaffTask completedTask)
        {
            _context.OnTaskCompleted(completedTask);
            servingOrder = null;
            _onMultitaskChefAvailable.Raise(_context);
            yield return _context.CtxMover.MoveTo(_context.RestPosition.position);
        }

        void LateUpdate() => _mover.SetZToZero();
    }
}