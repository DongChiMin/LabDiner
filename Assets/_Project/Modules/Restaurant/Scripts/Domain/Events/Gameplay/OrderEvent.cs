using LabDiner.Restaurant.Model;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "NewOrderEvent", menuName = "Events/Task/Order Event")]
    public class OrderEvent : GameEvent<Order> { }
}