using LabDiner.Shared.Event;
using LabDiner.Restaurant.Environment;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "OnStationAvailable", menuName = "Events/Table/Station Event")]
    public class StationEvent : GameEvent<Station> { }
}