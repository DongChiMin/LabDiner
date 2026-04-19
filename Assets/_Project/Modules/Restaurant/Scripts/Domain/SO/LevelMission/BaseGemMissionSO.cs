using LabDiner.Shared.Event;
using UnityEngine;

namespace LabDiner.Restaurant.SO
{
    // public enum MissionType
    // {
    //     UpgradeDish,      // Nâng cấp món a đến level b
    //     CollectTips,    // Nhặt số lượng a tiền tip
    //     HireChefs,      // Tổng số lượng đầu bếp đạt a
    // }

    public abstract class BaseGemMissionSO : ScriptableObject
    {
        [Header("Mission Info")]
        public string Title;
        public Sprite MissionIcon;
        public float MissionValue;

        [Header("Reward Info")]
        public Sprite RewardIcon;
        public float RewardValue;
        public LevelGemEvent OnGemAdded;

        public void ApplyReward()
        {
            // Gửi chính Asset này đi qua Event
            if (OnGemAdded != null)
                OnGemAdded.Raise(Mathf.RoundToInt(MissionValue));
        }
    }
}