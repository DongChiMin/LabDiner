using UnityEngine;
using UnityEngine.AI;

namespace LabDiner.Restaurant
{
    public class MultitaskChefContext : MonoBehaviour, IStaff
    { 
        [Header("Settings")]
        [SerializeField] private CookingTaskEvent _onCookingTaskComplete;
        [SerializeField] private OrderEvent _onOrderServed;
        [SerializeField] private Transform _restPosition;
        public Transform RestPosition => _restPosition;

        [Header("Components")]
        [SerializeField] private StaffMover _mover;
        [SerializeField] private MultitaskChefBehavior _behavior;
        [SerializeField] private MultitaskChefAI _ai;
        public StaffMover CtxMover => _mover;
        public MultitaskChefBehavior CtxBehavior => _behavior;
        public MultitaskChefAI CtxAI => _ai;
        public bool IsAvailable { get ; set ; } = true;

        public void DoTask(IStaffTask task)
        {
            IsAvailable = false;
            switch(task)
            {
                case CookingTask cookingTask:
                    _ai.StartCookAndShip(cookingTask);
                    break;
                case Order order:
                    _ai.StartServe(order);
                    return;
                default:
                    Debug.LogError("Unsupported task type for MultitaskChef: " + task.GetType());
                    break;
            }
        }

        //Hoàn thành sau khi chef bê đồ ăn tới passTable
        public void OnTaskCompleted(IStaffTask task)
        {
            IsAvailable = true;
            switch(task)
            {
                case CookingTask cookingTask:
                    _onCookingTaskComplete.Raise(cookingTask);
                    break;
                case Order order:
                    _onOrderServed.Raise(order);
                    break;
                default:
                    Debug.LogError("Unsupported task type for MultitaskChef: " + task.GetType());
                    break;
            }
        }
    }
}
