using UnityEngine;
using LabDiner.LevelSystem.Domain;
using System.Collections.Generic;
using LabDiner.Restaurant.SO;
using LabDiner.Restaurant.Interface;

namespace LabDiner.LevelSystem.Runtime
{
    public class LevelLoader : MonoBehaviour
    {
        [Header("Level Init")]
        [SerializeField] private Transform _levelRoot;
        [SerializeField] private Transform _esstensialInit;
        [SerializeField] private Transform _rule;

        [Header("Phase 2 Settings [Runtime]")]
        [SerializeField] private LevelConfigSO _config;

        // "Mỏ neo" để Phase 2 và 3 tìm đúng Object
        private Dictionary<string, GameObject> _spawnedInstances = new Dictionary<string, GameObject>();


        void Start()
        {
            LoadLevel( _config);
        }

        public void LoadLevel(LevelConfigSO configSO)
        {
            StopAllCoroutines();
            // Phase 1: Sinh ra thực thể
            // ExecutePhase1_SetupLayout(layoutSO.layoutData);

            // Phase 2: Bơm dữ liệu cấu hình
            ExecutePhase2_InitLogic(configSO);

            // Phase 3: Khôi phục tiến độ (Mồi cho cậu triển khai sau)
            // ExecutePhase3_RestoreProgress();
        }

        private void ExecutePhase2_InitLogic(LevelConfigSO configSO)
        {
            ILevelInitializable[] initializables = _levelRoot.GetComponentsInChildren<ILevelInitializable>();
            foreach (var init in initializables)
            {
                init.Init(configSO);
            }

            initializables = _esstensialInit.GetComponentsInChildren<ILevelInitializable>();
            foreach (var init in initializables)
            {
                init.Init(configSO);
            }

            initializables = _rule.GetComponentsInChildren<ILevelInitializable>();
            foreach (var init in initializables)
            {
                init.Init(configSO);
            }
        }
    }
}
