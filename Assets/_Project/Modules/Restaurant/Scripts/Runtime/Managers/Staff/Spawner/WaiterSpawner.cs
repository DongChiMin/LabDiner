using LabDiner.Restaurant.Humanoid;
using UnityEngine;

namespace LabDiner.Restaurant.Manager
{
    [RequireComponent(typeof(WaiterManager))]
    [RequireComponent(typeof(ShipManager))]
    public class WaiterSpawner : StaffSpawner<WaiterContext>
    {
    }
}