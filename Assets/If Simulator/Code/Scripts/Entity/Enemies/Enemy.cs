using UnityEngine;
using UnityEngine.AI;

public class Enemy : DamageabaleEntity
{
    [SerializeField] private NavMeshAgent _agent;
    
    public NavMeshAgent Agent => _agent;
    
    
    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }
}
