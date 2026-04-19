using System.Collections.Generic;
using LabDiner.Restaurant.Event;
using LabDiner.Restaurant.Model;
using UnityEngine;

namespace LabDiner.Restaurant.Manager
{
    public class ServeManager : MonoBehaviour
    {
        [SerializeField] private OrderEvent _onNewUnservedOrder;
        [Header("[DEBUG]")]
        [SerializeField] List<Order> serveRequests;

        void OnEnable()
        {
            _onNewUnservedOrder.Register(HandleNewOrder);
        }

        void OnDisable()
        {
            _onNewUnservedOrder.Unregister(HandleNewOrder);
        }

        void HandleNewOrder(Order order)
        {
            serveRequests.Add(order);
        }
    }
}