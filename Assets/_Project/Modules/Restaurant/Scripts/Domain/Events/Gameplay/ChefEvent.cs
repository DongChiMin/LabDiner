using LabDiner.Restaurant.Humanoid;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "OnChefAvailable", menuName = "Events/Staff/Chef Event")]
    public class ChefEvent : GameEvent<ChefContext> { }
}