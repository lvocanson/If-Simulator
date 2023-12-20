using UnityEngine;
using UnityEngine.AI;

public class Enemy : DamageableEntity
{
    [Header("References")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _objectToDestroyOnDeath;
    
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
        
        if (_objectToDestroyOnDeath != null)
            Destroy(_objectToDestroyOnDeath);
        else
            Destroy(gameObject);
    }
}
