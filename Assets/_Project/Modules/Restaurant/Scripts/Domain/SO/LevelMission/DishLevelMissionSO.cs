using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.SO
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "New Dish Mission", menuName = "Game/Missions/Dish Mission")]
    public class DishLevelMissionSO : BaseGemMissionSO
    {
        [Header("Target")]
        public DishSO TargetDish;
    }
}