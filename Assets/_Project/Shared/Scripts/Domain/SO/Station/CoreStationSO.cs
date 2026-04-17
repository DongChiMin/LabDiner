using System.Collections.Generic;
using UnityEngine;

namespace LabDiner.Shared.SO
{
    [CreateAssetMenu(fileName = "CoreStation", menuName = "Game/CoreStation")]
    public class CoreStationSO : ScriptableObject
    {
        public string Id => name; // Sử dụng tên của ScriptableObject làm ID

        [Header("Basic Info")]
        public DishSO Dish;

        [Header("Level Upgrade")]
        public int levelPerStar = 10;
        public List<StationStarSO> stationStars;

        [Header("Currency")]
        public double baseProfit = 10;
        public float baseProcessTime = 5f;
        public int baseUpgradeCost = 100;
        public AnimationCurve upgradeCostCurve;

        [Header("Final")]
        [ReadOnly] public int maxLevel;
        [ReadOnly] public int maxStar;

        private void OnValidate()
        {
            // Tự động tính toán lại mỗi khi thay đổi giá trị trong Inspector
            maxStar = stationStars.Count;
            maxLevel = maxStar * levelPerStar;

        }
    }
}