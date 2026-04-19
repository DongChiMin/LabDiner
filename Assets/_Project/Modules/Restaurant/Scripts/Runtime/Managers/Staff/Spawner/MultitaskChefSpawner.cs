using LabDiner.Restaurant.Humanoid;
using UnityEngine;

namespace LabDiner.Restaurant.Manager
{
    [RequireComponent(typeof(MultitaskChefCookingTaskManager))]
    [RequireComponent(typeof(MultitaskChefOrderManager))]
    public class MultitaskChefSpawner : StaffSpawner<MultitaskChefContext>
    {
    }
}