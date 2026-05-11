using LabDiner.Restaurant.Environment;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "NewDiningSeatEvent", menuName = "Events/GamePlay/Dining Seat Event")]
    public class DiningSeatEvent : GameEvent<DiningSeat> { }
}