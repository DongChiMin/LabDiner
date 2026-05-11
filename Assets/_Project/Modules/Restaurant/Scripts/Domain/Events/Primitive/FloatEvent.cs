using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "NewFloatEvent", menuName = "Events/Primitive/Float Event")]
    public class FloatEvent : GameEvent<float> { }
}