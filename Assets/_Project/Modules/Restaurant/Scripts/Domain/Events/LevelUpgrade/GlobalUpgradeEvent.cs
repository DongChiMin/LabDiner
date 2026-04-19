using LabDiner.Restaurant.SO;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "OnGlobalUpgrade", menuName = "Events/Upgrades/Global Upgrade Event")]
    public class GlobalUpgradeEvent : GameEvent<GlobalUpgradeSO> { }
}