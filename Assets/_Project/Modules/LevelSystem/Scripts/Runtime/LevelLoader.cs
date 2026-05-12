using UnityEngine;
using LabDiner.LevelSystem.Domain;
using System.Collections.Generic;
using LabDiner.Restaurant.SO;
using LabDiner.Restaurant.Interface;

namespace LabDiner.LevelSystem.Runtime
{
    public class LevelLoader : MonoBehaviour
    {
        [Header("Esstensials Level Initializables")]
        [Tooltip("Kéo thả những interface LevelInitializable cần Init từ đầu vào đây")]
        [SerializeField] private List<GameObject> _esstensials = new List<GameObject>();

        [Header("Phase 1 Settings")]
        [SerializeField] private PrefabRegistrySO _prefabRegistry;
        [SerializeField] private Transform _levelRoot;

        [Header("Phase 2 Settings [Runtime]")]
        [SerializeField] private LevelConfigSO _config;

        [Header("Testing")]
        [SerializeField] private LevelLayoutSO _testLayout;

        // "Mỏ neo" để Phase 2 và 3 tìm đúng Object
        private Dictionary<string, GameObject> _spawnedInstances = new Dictionary<string, GameObject>();


        void Start()
        {
            if (_testLayout != null)
            {
                ExecutePhase1_SetupLayout(_testLayout.layoutData);

                ExecutePhase2_InitLogic();
            }
        }

        public void LoadLevel(LevelLayoutSO layoutSO, LevelConfigSO configSO)
        {
            StopAllCoroutines();
            // Phase 1: Sinh ra thực thể
            ExecutePhase1_SetupLayout(layoutSO.layoutData);

            // Phase 2: Bơm dữ liệu cấu hình
            ExecutePhase2_InitLogic();

            // Phase 3: Khôi phục tiến độ (Mồi cho cậu triển khai sau)
            // ExecutePhase3_RestoreProgress();
        }

        private void ExecutePhase1_SetupLayout(LevelLayoutData data)
        {
            // 1. Dọn dẹp
            foreach (var inst in _spawnedInstances.Values) if (inst != null) Destroy(inst);
            _spawnedInstances.Clear();

            // 2. Sinh ra theo layout

            foreach (var objData in data.objects)
            {
                GameObject prefab = _prefabRegistry.GetPrefab(objData.prefabID);
                GameObject instance;

                if (prefab == null)
                {
                    instance = new GameObject(objData.instanceID);
                    instance.transform.SetParent(_levelRoot);
                }           
                else
                    instance = Instantiate(prefab, objData.position, Quaternion.Euler(objData.rotation), _levelRoot);

                instance.name = objData.instanceID;
                _spawnedInstances.Add(objData.instanceID, instance);

                // 3. Sắp xếp Hierarchy
                if (!string.IsNullOrEmpty(objData.parentGroup))
                {
                    //tìm trong danh sách vừa mới tạo
                    if (_spawnedInstances.TryGetValue(objData.parentGroup, out GameObject parentObj))
                    {
                        instance.transform.SetParent(parentObj.transform);
                    }
                    else
                    {
                        // Nếu không tìm thấy trong danh sách đã spawn, có thể nó là một group tĩnh
                        // hoặc group chưa được tạo. Ta xử lý tạm:
                        var group = new GameObject(objData.parentGroup);
                        group.transform.SetParent(_levelRoot);
                        instance.transform.SetParent(_levelRoot);

                        // Lưu group này vào Dictionary để lượt sau không phải Find nữa
                        if (!_spawnedInstances.ContainsKey(objData.parentGroup))
                            _spawnedInstances.Add(objData.parentGroup, group);
                    }
                }
            }
            Debug.Log($"Phase 1 Complete: {data.levelID} spawned.");
        }

        private void ExecutePhase2_InitLogic()
        {
            //cấu hình init cho các manager bàn ghế vừa spawwn, diningSeat, ...
            foreach (var obj in _spawnedInstances.Values)
            {
                obj.TryGetComponent(out ILevelInitializable initializable);
                if(initializable != null)
                {
                    initializable.Init(_config);
                }
            }

            //Cấu hình init cho các manager phụ thuộc (level, upgrade...)
            foreach (var system in _esstensials)
            {
                var initializables = system.GetComponents<ILevelInitializable>();
                if (initializables.Length > 0)                {
                    foreach (var initializable in initializables)
                    {
                        initializable.Init(_config);
                    }
                }
                else
                {
                    Debug.LogWarning($"GameObject {system.name} does not implement ILevelInitializable");
                }
            }
        }
    }
}
