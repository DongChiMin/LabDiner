using LabDiner.Restaurant.Humanoid;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "NewGuestEvent", menuName = "Events/GamePlay/Guest Event")]
    public class GuestEvent : GameEvent<GuestContext> { }
}