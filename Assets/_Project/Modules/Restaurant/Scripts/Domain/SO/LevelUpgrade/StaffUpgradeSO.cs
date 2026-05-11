using System.Collections.Generic;
using LabDiner.Restaurant.Enum;
using LabDiner.Restaurant.Event;
using LabDiner.Restaurant.Workflow;
using UnityEngine;

namespace LabDiner.Restaurant.SO
{
    
    // Nhóm 2: Nâng cấp dành riêng cho nhân viên
    //- Tăng profit
    //- Giảm thời gian làm việc
    [CreateAssetMenu(fileName = "New Staff Quantity Upgrade", menuName = "Game/Upgrades/Staff Quantity Upgrade")]
    public class StaffQuantityUpgradeSO : BaseUpgradeSO 
    {
        [Header("Events")]
        public StaffQuantityUpgradeEvent OnUpgradeRaised;
        public Staff staffPrefab;

        public override void ApplyUpgrade()
        {
            // Gửi chính Asset này đi qua Event
            if (OnUpgradeRaised != null)
                OnUpgradeRaised.Raise(this);
        }
    }
}