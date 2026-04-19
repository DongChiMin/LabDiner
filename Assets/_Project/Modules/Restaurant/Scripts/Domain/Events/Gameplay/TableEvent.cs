using System.Collections.Generic;
using LabDiner.Shared.Events;
using UnityEngine;

namespace LabDiner.Restaurant
{
    [CreateAssetMenu(fileName = "NewDiningSeatEvent", menuName = "Events/Table/Dining Seat Event")]
    public class TableEvent : GameEvent<DiningSeat> { }
}