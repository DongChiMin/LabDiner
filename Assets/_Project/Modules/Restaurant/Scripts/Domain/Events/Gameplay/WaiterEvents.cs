using LabDiner.Restaurant.Humanoid;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "OnWaiterAvailable", menuName = "Events/Staff/Waiter Event")]
    public class WaiterEvent : GameEvent<WaiterContext> { }
}