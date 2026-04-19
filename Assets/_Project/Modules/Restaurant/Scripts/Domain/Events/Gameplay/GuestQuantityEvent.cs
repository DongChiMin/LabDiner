using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "OnGuestQuantityChanged", menuName = "Events/Guest/Guest Quantity Event")]
    public class GuestQuantityEvent : GameEvent<int> { }
}