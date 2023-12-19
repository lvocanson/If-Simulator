using UnityEngine;
using UnityEngine.AI;

public class Enemy : DamageableEntity
{
    [SerializeField] private NavMeshAgent _agent;
    
    public NavMeshAgent Agent => _agent;
    
    
    protected override void Start()
    {
        base.Start();
        
        _agent.baseOffset = -0.005293157f;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    protected override void Die()
    {
        base.Die();
        
        Destroy(gameObject);
    }
}
