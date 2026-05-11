using LabDiner.Restaurant.SO;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "OnLevelUpgrade", menuName = "Events/Upgrades/Level Upgrade Event")]
    public class LevelUpgradeEvent : GameEvent<LevelUpgradeSO> { }
}