using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "NewReputationEvent", menuName = "Events/Task/Reputation Event")]
    public class ReputationEvent : GameEvent<float> { }
}