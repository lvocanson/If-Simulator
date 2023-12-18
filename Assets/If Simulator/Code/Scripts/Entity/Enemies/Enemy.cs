using UnityEngine;
using UnityEngine.AI;

public class Enemy : DamageableEntity
{
    [SerializeField] private NavMeshAgent _agent;
    
    public NavMeshAgent Agent => _agent;
    
    
    protected override void Start()
    {
        base.Start();
        
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    protected override void Die()
    {
        base.Die();
        
        Destroy(gameObject);
    }
}
