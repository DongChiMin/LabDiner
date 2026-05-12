using UnityEngine;

namespace LabDiner.LevelSystem.Domain
{
    [CreateAssetMenu(fileName = "Layout_Level_", menuName = "SO/Level/Layout")]
    public class LevelLayoutSO : ScriptableObject
    {
        public LevelLayoutData layoutData;
    }
}
