using LabDiner.Restaurant.SO;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "NewBoolEvent", menuName = "Events/Primitive/Bool Event")]
    public class BoolEvent : GameEvent<bool> { }
}