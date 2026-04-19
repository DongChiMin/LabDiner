using System.Collections.Generic;
using LabDiner.Restaurant.Interface;
using LabDiner.Restaurant.SO;
using UnityEngine;

namespace LabDiner.Shared.Runtime.Manager
{
        public class LevelConfigBootstrap : MonoBehaviour
    {
        [SerializeField] private LevelConfigSO _levelConfigSO;
        [SerializeField] private List<MonoBehaviour> _managers = new List<MonoBehaviour>();

        private void Start()
        {            
            foreach (var obj in _managers)
            {
                if (obj is ILevelInitializable system)
                {
                    system.Init(_levelConfigSO);
                }
                else
                {
                    Debug.LogWarning($"Object {obj.name} does not implement ILevelInitializable");
                }
            }
        }
    }
}
