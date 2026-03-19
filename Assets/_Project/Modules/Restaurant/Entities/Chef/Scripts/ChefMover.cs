using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace LabDiner.Restaurant
{
    public class ChefMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _speed = 5f;

        void Start()
        {
            _agent.speed = _speed;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        public IEnumerator MoveTo(Vector3 destination)
        {
            _agent.SetDestination(destination);

            yield return new WaitUntil(() => !_agent.pathPending);

            while (_agent.remainingDistance > _agent.stoppingDistance)
            {
                yield return null;
            }
        }

        void LateUpdate()
        {
            // Đảm bảo Z luôn bằng 0 cho game 2D
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}