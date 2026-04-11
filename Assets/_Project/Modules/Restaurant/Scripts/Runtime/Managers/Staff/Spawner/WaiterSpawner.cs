
using System.Collections.Generic;
using LabDiner.Shared;
using LabDiner.Shared.SO;
using UnityEngine;

namespace LabDiner.Restaurant
{
    public class WaiterSpawner : MonoBehaviour
    {
        [SerializeField] private LevelUpgradeEvent _onUpgradeWaiterQuantity;
        [SerializeField] private WaiterContext _waiterContextPrefab;
        [SerializeField] private List<Transform> _restPositions;
        [SerializeField] private WaiterManager _waiterManager;
        [SerializeField] private ShipManager _shipManager;
        [SerializeField] private int _initialWaiterCount = 1;
        private List<WaiterContext> _waiters = new List<WaiterContext>();

        void Start()
        {
            SpawnWaiter(_initialWaiterCount);
        }

        private void OnEnable()
        {
            _onUpgradeWaiterQuantity.Register(HandleUpgradeWaiterQuantity);
        }

        private void OnDisable()
        {
            _onUpgradeWaiterQuantity.Unregister(HandleUpgradeWaiterQuantity);
        }

        private void HandleUpgradeWaiterQuantity(BaseUpgradeSO upgradeSO)
        {
            int amount = Mathf.RoundToInt(upgradeSO.UpgradeValue);
            SpawnWaiter(amount);
        }

        private void SpawnWaiter(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                int index = _waiters.Count;
                Transform restPoint = _restPositions[index % _restPositions.Count];

                if (index >= _restPositions.Count)
                {
                    Debug.LogWarning("Not enough rest positions for new waiter. Consider adding more rest positions.");
                }

                WaiterContext waiter = Instantiate(_waiterContextPrefab, restPoint.position, Quaternion.identity, transform);
                waiter.RestPosition = restPoint;
                _waiters.Add(waiter);

                _waiterManager.AssignNewStaff(waiter);
                _shipManager.AssignNewStaff(waiter);
            }
        }
    }
}