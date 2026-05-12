using UnityEngine;
using System.Collections.Generic;

namespace LabDiner.LevelSystem.Domain
{
    [CreateAssetMenu(fileName = "PrefabRegistry", menuName = "SO/Level/PrefabRegistry")]
    public class PrefabRegistrySO : ScriptableObject
    {
        [System.Serializable]
        public struct PrefabMapping
        {
            public string prefabID;
            public GameObject prefab;
        }

        public List<PrefabMapping> registry;

        public GameObject GetPrefab(string id)
        {
            var mapping = registry.Find(x => x.prefabID == id);
            return mapping.prefab != null ? mapping.prefab : null;
        }
    }
}