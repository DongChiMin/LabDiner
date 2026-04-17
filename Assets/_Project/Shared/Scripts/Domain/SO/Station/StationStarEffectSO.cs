
using LabDiner.Shared.Enum;
using UnityEngine;

namespace LabDiner.Shared.SO
{
    /// <summary>
    /// Các hiệu ứng khi station đạt được 1 sao bất kỳ
    /// </summary>
    public abstract class StationStarEffectSO : ScriptableObject
    {
        public StationStarEffect effectType;
        public float value;
    }
}