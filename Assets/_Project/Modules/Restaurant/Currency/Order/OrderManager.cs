using System.Collections.Generic;
using UnityEngine;

namespace LabDiner.Restaurant
{
    public class OrderManager : MonoBehaviour
    {
        public static OrderManager Instance { get; private set; }

        private Queue<Order> _pendingOrders = new Queue<Order>();

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        // Guest sẽ gọi hàm này
        public void AddOrder(Order newOrder)
        {
            _pendingOrders.Enqueue(newOrder);
            Debug.Log($"<color=cyan>[OrderManager]</color> Nhận đơn: {newOrder.DishName}");
        }

        // Chef sẽ check cái này
        public bool HasPendingOrder() => _pendingOrders.Count > 0;

        // Chef lấy đơn ra làm
        public Order GetNextOrder() => _pendingOrders.Dequeue();
    }
}