
using LabDiner.Shared.Input;
using UnityEngine;

namespace LabDiner.Restaurant
{
    public class StaffBox : MonoBehaviour, IInteractable
    {
        private ChefContext _chef;
        private ChefSpawner _spawner;
        public void Setup(ChefContext chef, ChefSpawner spawner)
        {
            _chef = chef;
            _spawner = spawner;
        }
        public bool CanInteract()
        {
            return true;
        }

        public void OnInteract()
        {
            _spawner.UnboxStaff(_chef);
            PoolContext.Instance.StaffBoxPool.ReturnToPool(this);
        }
    }
}