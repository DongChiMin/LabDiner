using UnityEngine;

namespace LabDiner.Restaurant.SO
{
    // public enum UpgradeType
    // {
    //     CookingSpeed,      // Nấu nhanh hơn
    //     StaffMoveSpeed,    // Nhân viên chạy nhanh
    //     ChefQuantity,      // + Số lượng đầu bếp
    //     MultitaskChefQuantity, // + Số lượng đầu bếp có thể làm nhiều món cùng lúc
    //     WaiterQuantity,    // + Số lượng phục vụ
    //     GuestCapacity,     // + Số lượng khách
    //     DishProfitMultiplier,    // x3 Profit 1 món (cần thêm ID món)
    //     GlobalProfitMultiplier   // x3 Profit toàn bộ
    // }

    public abstract class BaseMissionSO : ScriptableObject
    {
        [Header("Mission Info")]
        public string Title;
        public Sprite MissionIcon;
        public Sprite RewardIcon;
        public float MissionValue;
        public float RewardValue;

        public abstract void ApplyReward();
    }
}