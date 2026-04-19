using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.SO
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "New Gem Mission", menuName = "Game/Missions/Gem Mission")]
    public class GemMissionSO : BaseMissionSO
    {
        [Header("Events")]
        public LevelGemEvent OnGemAdded;
        public DishSO Dish;

        public override void ApplyReward()
        {
            // Gửi chính Asset này đi qua Event
            if (OnGemAdded != null)
                OnGemAdded.Raise(Mathf.RoundToInt(MissionValue));
        }
    }
}