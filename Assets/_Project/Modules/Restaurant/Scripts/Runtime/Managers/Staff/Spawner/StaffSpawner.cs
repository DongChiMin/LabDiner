using System.Collections.Generic;
using LabDiner.Restaurant.Event;
using LabDiner.Restaurant.Interface;
using LabDiner.Restaurant.Pooling;
using LabDiner.Restaurant.SO;
using LabDiner.Restaurant.Workflow;
using UnityEngine;

namespace LabDiner.Restaurant.Manager
{
    public partial class StaffSpawner : MonoBehaviour, IStaffUnboxer
    {
        [Header("Events")]
        [SerializeField] private GlobalUpgradeEvent _onUpgradeAllStaffMoveSpeed;

        [Header("Base Settings")]
        [SerializeField] protected GlobalUpgradeEvent _onUpgradeQuantity;
        [SerializeField] protected Staff _staffPrefab;
        [SerializeField] private Transform _spawnParent;

        [SerializeField] protected List<Transform> _restPositions;
        [SerializeField] protected int _initialCount = 1;
        [SerializeField] protected bool _spawnInBox = true;

        [Header("[Runtime]")]

        [SerializeField] protected List<Staff> _spawnedStaffs = new List<Staff>();

        void Start() => Spawn(_initialCount);

        void OnEnable()
        {
            _onUpgradeQuantity.Register(HandleUpgrade);
            _onUpgradeAllStaffMoveSpeed.Register(HandleUpgradeAllStaffMoveSpeed);

        }
        void OnDisable()
        {
            _onUpgradeQuantity.Unregister(HandleUpgrade);
            _onUpgradeAllStaffMoveSpeed.Unregister(HandleUpgradeAllStaffMoveSpeed);
        }

        private void HandleUpgrade(BaseUpgradeSO upgradeSO)
        {
            int amount = Mathf.RoundToInt(upgradeSO.UpgradeValue);
            OnUpgradeReceived(amount);
        }

        protected Staff CreateInstance()
        {
            int index = _spawnedStaffs.Count;
            Transform restPoint = _restPositions[index % _restPositions.Count];

            if (index >= _restPositions.Count)
                Debug.LogWarning($"Not enough rest positions for {typeof(Staff).Name}!");

            Staff staff = Instantiate(_staffPrefab, restPoint.position, Quaternion.identity, _spawnParent);
            staff.RestPosition = restPoint;
            _spawnedStaffs.Add(staff);
            return staff;
        }

        void Spawn(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                Staff staff = CreateInstance();
                staff.gameObject.SetActive(true);
            }
        }

        void OnUpgradeReceived(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                Staff staff = CreateInstance();

                if (_spawnInBox)
                {
                    staff.gameObject.SetActive(false);
                    var box = PoolContext.Instance.StaffBoxPool.Get(staff.RestPosition.position, Quaternion.identity);
                    box.Setup(staff, this);
                }
                else
                {
                    UnboxStaff(staff);
                }
            }
        }

        public void UnboxStaff(Component staff)
        {
            if (staff is Staff concreteStaff)
            {
                concreteStaff.gameObject.SetActive(true);
            }
        }

        private void HandleUpgradeAllStaffMoveSpeed(BaseUpgradeSO upgradeSO)
        {
            foreach (IStaff staff in _spawnedStaffs)
            {
                staff.UpgradeMoveSpeed(upgradeSO.UpgradeValue);
            }
        }

        
    }
}