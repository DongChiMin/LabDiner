using LabDiner.Restaurant.Event;
using UnityEngine;

namespace LabDiner.Restaurant.SO
{
    
    // Nhóm 2: Nâng cấp dành riêng cho món ăn
    //- Tăng profit
    //- Giảm thời gian nấu
    [CreateAssetMenu(fileName = "New Level Upgrade", menuName = "Game/Upgrades/Level Upgrade")]
    public class LevelUpgradeSO : BaseUpgradeSO 
    {
        [Header("Events")]
        public LevelUpgradeEvent OnUpgradeRaised;

        public override void ApplyUpgrade()
        {
            // Gửi chính Asset này đi qua Event
            if (OnUpgradeRaised != null)
                OnUpgradeRaised.Raise(this);
        }
    }
}