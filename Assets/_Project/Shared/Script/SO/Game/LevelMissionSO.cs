using UnityEngine;

namespace LabDiner.Shared
{

    [CreateAssetMenu(fileName = "NewMission", menuName = "Game/Mission")]
    public class LevelMissionSO : ScriptableObject
    {
        [Header("Mission Details")]
        public string missionDescription; // "Nâng cấp Hotdog lên level 30"
        public MissionType type;          // Enum: Upgrade, Sell, Unlock
        public DishSO targetID;           // SO của mục tiêu (ví dụ: "hotdog")
        public int targetValue;           // Giá trị cần đạt (ví dụ: 30)

        [Header("Reward")]        
        public int goldReward;
    }
}
