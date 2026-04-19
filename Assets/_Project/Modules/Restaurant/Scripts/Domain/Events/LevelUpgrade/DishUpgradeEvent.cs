using LabDiner.Restaurant.SO;
using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.Event
{
    [CreateAssetMenu(fileName = "OnDishUpgrade", menuName = "Events/Upgrades/Dish Upgrade Event")]
    public class DishUpgradeEvent : GameEvent<DishUpgradeSO> { }
}