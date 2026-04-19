using LabDiner.Restaurant.Model;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "NewCookingTaskEvent", menuName = "Events/Task/Cooking Task Event")]
    public class CookingTaskEvent : GameEvent<CookingTask> { }
}