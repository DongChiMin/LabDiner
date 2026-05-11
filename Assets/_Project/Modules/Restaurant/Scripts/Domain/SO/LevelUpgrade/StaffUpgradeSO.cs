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
    [CreateAssetMenu(fileName = "New Staff Upgrade", menuName = "Game/Upgrades/Staff Upgrade")]
    public class StaffUpgradeSO : BaseUpgradeSO 
    {
        [Header("Events")]
        public StaffUpgradeEvent OnUpgradeRaised;
        public List<TaskType> staffSkill;

        public override void ApplyUpgrade()
        {
            // Gửi chính Asset này đi qua Event
            if (OnUpgradeRaised != null)
                OnUpgradeRaised.Raise(this);
        }
    }
}