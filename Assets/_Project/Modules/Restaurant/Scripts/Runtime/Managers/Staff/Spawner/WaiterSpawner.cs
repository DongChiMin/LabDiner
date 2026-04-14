using UnityEngine;

namespace LabDiner.Restaurant
{
    [RequireComponent(typeof(WaiterManager))]
    [RequireComponent(typeof(ShipManager))]
    public class WaiterSpawner : StaffSpawner<WaiterContext>
    {
    }
}