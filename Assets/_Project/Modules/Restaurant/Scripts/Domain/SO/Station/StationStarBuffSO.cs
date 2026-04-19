using LabDiner.Restaurant.Enum;
using UnityEngine;

namespace LabDiner.Restaurant.SO
{
    /// <summary>
    /// Các hiệu ứng khi station đạt được 1 sao bất kỳ
    /// </summary>
    [CreateAssetMenu(fileName = "StationStarBuff", menuName = "Game/Station/StationStarBuff")]
    public class StationStarBuffSO : ScriptableObject
    {
        public StationStarBuffType EffectType;
        public float Value; 
    }
}