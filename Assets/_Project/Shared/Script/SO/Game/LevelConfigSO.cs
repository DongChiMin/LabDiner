using System.Collections.Generic;
using UnityEngine;

namespace LabDiner.Shared
{
    [CreateAssetMenu(fileName = "Level Config", menuName = "Game/Level Config")]
    public class LevelConfigSO : ScriptableObject
    {
        public int levelIndex;
        public string levelName;

        [Header("Available Resources")]
        public List<IngredientSO> availableItems; // Danh sách nguyên liệu cấp cho người chơi ở Level này

        [Header("Goals")]
        public List<FlavorTag> targetDemands;      // Khẩu vị khách ở level này (ví dụ: Spicy)
        public List<LevelMissionSO> missions;       // Danh sách nhiệm vụ cụ thể (ví dụ: "Phục vụ 5 món Spicy", "Phục vụ 3 món Sweet", v.v.)
    }
}