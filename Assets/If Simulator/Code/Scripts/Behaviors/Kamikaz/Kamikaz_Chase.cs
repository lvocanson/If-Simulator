using UnityEngine;
using FiniteStateMachine;

public class Kamikaz_Chase : BaseState
{
    [SerializeField] Enemy _enemy;

    [Header("State Machine")]
    [SerializeField] private Kamikaz_Patrol _patrolState;
    [SerializeField] private Kamikaz_Attack _attackState;
    
    [Header("Data")]
    [SerializeField] private float _speed = 1f;
    
    [Header("Events")]
    [SerializeField] private PhysicsEvents _attackColEvent;
    [SerializeField] private PhysicsEvents _chaseColEvent;
    
    private Transform _target;

    
    public void SetTarget(Transform target)
    {
        _target = target;
        _enemy.Agent.SetDestination(_target.position);
    }
    
    private void OnEnable()
    {
        _chaseColEvent.OnExit += ExitOnChaseRange;
        _attackColEvent.OnEnter += EnterOnAttackRange;
        _enemy.Agent.speed = _speed;
    }

    private void EnterOnAttackRange(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            Manager.ChangeState(_attackState);
        }
    }

    private void ExitOnChaseRange(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
            Manager.ChangeState(_patrolState);
    }

    private void OnDisable()
    {
        _chaseColEvent.OnExit -= ExitOnChaseRange;
        _attackColEvent.OnEnter -= EnterOnAttackRange;
    }
}
