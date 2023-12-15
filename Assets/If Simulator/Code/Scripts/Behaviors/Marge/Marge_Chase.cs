using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;
using SAP2D;

public class Marge_Chase : BaseState
{
    [ShowNonSerializedField, Tooltip("The target to move towards")] private Transform _target;
    
    [Header("References")]
    [SerializeField] private CircleCollider2D _chaseCol;
    [SerializeField] SAP2DAgent _SAPAgent;

    [Header("State Machine")]
    [SerializeField] private Marge_Patrol _patrolState;
    [SerializeField] private Marge_Attack _attackState;
    
    [Header("Data")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _chaseRange = 2f;
    
    [Header("Events")]
    [SerializeField] private PhysicsEvents _attackColEvent;
    [SerializeField] private PhysicsEvents _chaseColEvent;
    
    public void SetTarget(Transform target) => _target = target;
    
    
    private void Awake()
    {
        _chaseCol.radius = _chaseRange;
    }
    
    private void OnEnable()
    {
        _chaseColEvent.OnExit += ExitOnChaseRange;
        _attackColEvent.OnEnter += EnterOnAttackRange;
        
        _SAPAgent.Target = _target;
        _SAPAgent.MovementSpeed = _speed;
    }

    private void EnterOnAttackRange(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            _attackState.SetTarget(obj.transform);
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
        _chaseColEvent.OnEnter -= ExitOnChaseRange;
        _attackColEvent.OnEnter -= EnterOnAttackRange;
    }
}
