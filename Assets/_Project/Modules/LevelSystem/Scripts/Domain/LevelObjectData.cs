using System.Collections.Generic;
using UnityEngine;

namespace LabDiner.LevelSystem.Domain
{
    [System.Serializable]
    public class LevelObjectData
    {
        public string instanceID;   // ID duy nhất trong level (ví dụ: "Oven_01")
        public string prefabID;     // ID để tìm prefab (ví dụ: "Station_Oven")
        public Vector3 position;
        public Vector3 rotation;
        public string parentGroup;  // Nhóm phân cấp (ví dụ: "Stations", "Decor")

        // --- Object này thuộc tính năng nào? ---
        // Nếu để None, nó sẽ luôn luôn được sinh ra (Core gameplay).
        public LevelFeature requiredFeature = LevelFeature.None;
    }
}
