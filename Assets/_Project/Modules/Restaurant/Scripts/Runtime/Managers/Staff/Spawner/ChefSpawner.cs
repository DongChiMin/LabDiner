
using System.Collections.Generic;
using LabDiner.Shared;
using LabDiner.Shared.SO;
using UnityEngine;

namespace LabDiner.Restaurant
{
    public class ChefSpawner : MonoBehaviour
    {
        [SerializeField] private LevelUpgradeEvent _onUpgradeChefQuantity;
        [SerializeField] private ChefContext _chefContextPrefab;
        [SerializeField] private List<Transform> _restPositions;
        [SerializeField] private ChefManager _chefManager;
        [SerializeField] private int _initialChefCount = 1;
        private List<ChefContext> _chefs = new List<ChefContext>();

        void Start()
        {
            SpawnChef(_initialChefCount);
        }

        private void OnEnable()
        {
            _onUpgradeChefQuantity.Register(HandleUpgradeChefQuantity);
        }

        private void OnDisable()
        {
            _onUpgradeChefQuantity.Unregister(HandleUpgradeChefQuantity);
        }

        private void HandleUpgradeChefQuantity(BaseUpgradeSO upgradeSO)
        {
            int amount = Mathf.RoundToInt(upgradeSO.UpgradeValue);
            SpawnStaffBox(amount);
        }

        private void SpawnChef(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                int index = _chefs.Count;
                Transform restPoint = _restPositions[index % _restPositions.Count];

                if (index >= _restPositions.Count)
                {
                    Debug.LogWarning("Not enough rest positions for new chef. Consider adding more rest positions.");
                }

                ChefContext chef = Instantiate(_chefContextPrefab, restPoint.position, Quaternion.identity, transform);
                chef.RestPosition = restPoint;
                _chefs.Add(chef);

                _chefManager.AssignNewStaff(chef);
            }
        }

        private void SpawnStaffBox(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                int index = _chefs.Count;
                Transform restPoint = _restPositions[index % _restPositions.Count];

                if (index >= _restPositions.Count)
                {
                    Debug.LogWarning("Not enough rest positions for new chef. Consider adding more rest positions.");
                }

                ChefContext chef = Instantiate(_chefContextPrefab, restPoint.position, Quaternion.identity, transform);
                chef.RestPosition = restPoint;
                _chefs.Add(chef);
                
                chef.gameObject.SetActive(false);
                StaffBox box = PoolContext.Instance.StaffBoxPool.Get(restPoint.position, Quaternion.identity);
                box.Setup(chef, this);
            }
        }

        public void UnboxStaff(ChefContext chef)
        {
            chef.gameObject.SetActive(true);
            _chefManager.AssignNewStaff(chef);
        }
    }
}