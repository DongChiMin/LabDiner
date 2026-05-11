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
        [SerializeField] private LevelUpgradeEvent _onUpgradeAllStaffMoveSpeed;

        [Header("Base Settings")]
        [SerializeField] protected StaffQuantityUpgradeEvent _onUpgradeStaffQuantity;
        [SerializeField] protected List<Staff> _initialStaffs;
        [SerializeField] private Transform _spawnParent;

        [SerializeField] protected List<Transform> _restPositions;
        [SerializeField] protected bool _spawnInBox = true;

        [Header("[Runtime]")]

        [SerializeField] protected List<Staff> _spawnedStaffs = new List<Staff>();

        void Start()
        {
            foreach (Staff staffPrefab in _initialStaffs)
            {
                Staff staff = CreateInstance(staffPrefab);
                staff.gameObject.SetActive(true);
            }
        }

        void OnEnable()
        {
            _onUpgradeStaffQuantity.Register(HandleUpgradeQUantity);
            _onUpgradeAllStaffMoveSpeed.Register(HandleUpgradeAllStaffMoveSpeed);

        }
        void OnDisable()
        {
            _onUpgradeStaffQuantity.Unregister(HandleUpgradeQUantity);
            _onUpgradeAllStaffMoveSpeed.Unregister(HandleUpgradeAllStaffMoveSpeed);
        }

        private void HandleUpgradeQUantity(StaffQuantityUpgradeSO upgradeSO)
        {
            int quantity = Mathf.RoundToInt(upgradeSO.UpgradeValue);
            
            for (int i = 0; i < quantity; i++)
            {
                Staff staff = CreateInstance(upgradeSO.staffPrefab);

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

        protected Staff CreateInstance(Staff prefab)
        {
            int index = _spawnedStaffs.Count;
            Transform restPoint = _restPositions[index % _restPositions.Count];

            if (index >= _restPositions.Count)
                Debug.LogWarning($"Not enough rest positions for {typeof(Staff).Name}!");

            Staff staff = Instantiate(prefab, restPoint.position, Quaternion.identity, _spawnParent);
            staff.RestPosition = restPoint;
            _spawnedStaffs.Add(staff);
            return staff;
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