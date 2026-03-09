using UnityEngine;
using UnityEngine.AI;

public class SpikePathing : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Transform[] _patrolPoints;
    private int _currentIndex = 0;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        if(_patrolPoints.Length > 0)
        {
            _agent.SetDestination(_patrolPoints[_currentIndex].position);
        }
    }

    private void Update()
    {
        if (_patrolPoints.Length == 0) return;
        if(!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _currentIndex = (_currentIndex + 1) % _patrolPoints.Length;
            _agent.SetDestination(_patrolPoints[_currentIndex].position);
        }
    }
}
