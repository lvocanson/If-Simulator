using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;

public class Tank_Chase : BaseState
{
    [ShowNonSerializedField, Tooltip("The target to move towards")] private Transform _target;

    [Header("References")]
    [SerializeField] private CircleCollider2D _chaseCol;
    [SerializeField] Enemy _enemy;

    [Header("State Machine")]
    [SerializeField] private Tank_Patrol _patrolState;
    [SerializeField] private Tank_Attack _attackState;

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

        _enemy.Agent.SetDestination(_target.position);
        _enemy.Agent.speed = _speed;
    }

    private void EnterOnAttackRange(Collider2D obj)
    {
        if (!obj.CompareTag("Player") || !obj.GetComponent<Player>()) return;
        
        _attackState.SetTarget(obj.transform);
        Manager.ChangeState(_attackState);
    }

    private void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, _enemy.Agent.velocity);
    }

    private void ExitOnChaseRange(Collider2D obj)
    {
        if (!obj.CompareTag("Player") || !obj.GetComponent<Player>()) return;

        Manager.ChangeState(_patrolState);
    }

    private void OnDisable()
    {
        _chaseColEvent.OnExit -= ExitOnChaseRange;
        _attackColEvent.OnEnter -= EnterOnAttackRange;
    }
}
