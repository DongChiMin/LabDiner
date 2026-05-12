using System.Collections.Generic;
using UnityEngine;

namespace LabDiner.LevelSystem.Domain
{
    [System.Serializable]
    public class LevelLayoutData
    {
        public string levelID;
        public List<LevelObjectData> objects;
        public List<LevelFeature> enabledFeatures;
    }
}
