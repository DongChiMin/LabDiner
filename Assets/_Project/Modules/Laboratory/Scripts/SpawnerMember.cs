using UnityEngine;

namespace LabDiner.Laboratory
{
    public class SpawnerMember : MonoBehaviour
    {
        private LabIngredientSpawner _originSpawner;

        public void SetOrigin(LabIngredientSpawner spawner) => _originSpawner = spawner;

        public void ReturnToSpawner() 
        {
        if (_originSpawner != null) 
        {
            _originSpawner.ToggleSpawner(true);
        }
    }
    }
}