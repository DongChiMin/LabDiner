using LabDiner.Restaurant.Event;
using UnityEngine;

namespace LabDiner.Restaurant.SO
{
    
    // Nhóm 2: Nâng cấp dành riêng cho món ăn
    //- Tăng profit
    //- Giảm thời gian nấu
    [CreateAssetMenu(fileName = "New Dish Upgrade", menuName = "Game/Upgrades/Dish Upgrade")]
    public class DishUpgradeSO : BaseUpgradeSO 
    {
        [Header("Events")]
        public DishUpgradeEvent OnUpgradeRaised;
        public DishSO Dish;

        public override void ApplyUpgrade()
        {
            // Gửi chính Asset này đi qua Event
            if (OnUpgradeRaised != null)
                OnUpgradeRaised.Raise(this);
        }
    }
}