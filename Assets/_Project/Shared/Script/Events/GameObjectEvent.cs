using LabDiner.Shared.Events;
using UnityEngine;

namespace LabDiner.Shared
{
    [CreateAssetMenu(fileName = "GameObjectEvent", menuName = "Events/Game Object Event")]
    public class GameObjectEvent : GameEvent<GameObject> { }
}
