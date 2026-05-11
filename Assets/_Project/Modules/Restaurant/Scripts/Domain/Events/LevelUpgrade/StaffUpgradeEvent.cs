using LabDiner.Restaurant.SO;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "OnStaffUpgrade", menuName = "Events/Upgrades/Staff Upgrade Event")]
    public class StaffUpgradeEvent : GameEvent<StaffUpgradeSO> { }
}