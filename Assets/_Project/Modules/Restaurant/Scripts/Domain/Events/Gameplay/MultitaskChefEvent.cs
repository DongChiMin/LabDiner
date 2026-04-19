using LabDiner.Restaurant.Humanoid;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "OnMultitaskChefAvailable", menuName = "Events/Staff/Multitask Chef Event")]
    public class MultitaskChefEvent : GameEvent<MultitaskChefContext> { }
}