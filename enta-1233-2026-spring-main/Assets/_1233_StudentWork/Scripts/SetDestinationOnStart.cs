using UnityEngine;

[RequireComponent(typeof(NavMeshAgentMover))]

public class SetDestinationOnStart : MonoBehaviour
{
    [SerializeField] private NavMeshAgentMover _agent;
    [SerializeField] private Transform _patrolPoints;
    private int _currentCheckPointIndex;
    private void Start()
    {
        
    }
    public void NextPatrolPoint()
    {
        _currentCheckPointIndex++;
    }
    public void RestartPatrolCount()
    {
        _currentCheckPointIndex = 0;
    }
    private void Update()
    {
        _agent.SetDestination(_patrolPoints.position);
    }
}
