using LabDiner.Restaurant.Humanoid;
using UnityEngine;

namespace LabDiner.Restaurant.Manager
{
    [RequireComponent(typeof(ChefManager))]
    public class ChefSpawner : StaffSpawner<ChefContext>
    {
    }
}