using LabDiner.Restaurant.SO;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "OnUpgradeStaffQuantity", menuName = "Events/Upgrades/Staff Quantity Upgrade Event")]
    public class StaffQuantityUpgradeEvent : GameEvent<StaffQuantityUpgradeSO> { }
}