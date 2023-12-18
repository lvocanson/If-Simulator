using UnityEngine;
using FiniteStateMachine;

public class Sprinter_Chase : BaseState
{
    [SerializeField] Enemy _enemy;
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    
    [Header("State Machine")]
    [SerializeField] private BaseState _patrolState;
    [SerializeField] private BaseState _attackState;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _chaseRange = 2f;
    [SerializeField] private float _attackRange = 0.1f;

    private void OnEnable()
    {
        Debug.Log("Focus : " + _target.name + " OnEnabled.");
        _enemy.Agent.SetDestination(_target.position);
        _enemy.Agent.speed = _speed;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _target.position) > _chaseRange)
        {
            Manager.ChangeState(_patrolState);
        }
        else if (Vector3.Distance(transform.position, _target.position) < _attackRange)
        {
            Manager.ChangeState(_attackState);
        }
    }
}
