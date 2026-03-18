using UnityEngine;
using UnityEngine.AI;

namespace LabDiner.Restaurant
{
    public class ChefMovement : MonoBehaviour
    { 
        [SerializeField] GameObject target;
        [SerializeField] NavMeshAgent agent;

        private void Start()
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
        private void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.transform.position);
            }
        }

        void LateUpdate() {
        // ĐẢM BẢO TRỤC Z LUÔN BẰNG 0 (Tránh sai lệch do tính toán đường đi)
        if (transform.position.z != 0) {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
    }
}
