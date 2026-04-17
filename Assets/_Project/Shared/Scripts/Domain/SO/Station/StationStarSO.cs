using System.Collections.Generic;
using UnityEngine;

namespace LabDiner.Shared.SO
{
    [CreateAssetMenu(fileName = "StationStar", menuName = "Game/StationStar")]
    public class StationStarSO : ScriptableObject
    {
        public Sprite starIcon;
        public List<StationStarEffectSO> effects;   //ví dụ cột mốc này vừa x2 profit, vừa tạo station mới
    }
}