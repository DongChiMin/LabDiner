using LabDiner.Restaurant.Environment;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "NewDiningSeatEvent", menuName = "Events/Table/Dining Seat Event")]
    public class TableEvent : GameEvent<DiningSeat> { }
}