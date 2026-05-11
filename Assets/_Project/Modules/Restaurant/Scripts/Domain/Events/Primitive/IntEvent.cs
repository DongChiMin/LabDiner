using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "OnIntValueChanged", menuName = "Events/Primitive/Int Event")]
    public class IntEvent : GameEvent<int> { }
}