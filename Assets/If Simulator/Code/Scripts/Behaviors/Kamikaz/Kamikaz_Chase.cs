using System;
using System.Collections;
using UnityEngine;
using FiniteStateMachine;

public class Kamikaz_Chase : BaseState
{
    [SerializeField] Enemy _enemy;

    [Header("State Machine")]
    [SerializeField] private Kamikaz_Attack _attackState;
    
    [Header("Data")]
    [SerializeField] private float _speed = 1f;
    
    [Header("Events")]
    [SerializeField] private PhysicsEvents _attackColEvent;
    
    private Transform _target;

    
    public void SetTarget(Transform target)
    {
        _target = target;
        _enemy.Agent.SetDestination(_target.position);
    }
    
    private void OnEnable()
    {
        _attackColEvent.OnEnter += EnterOnAttackRange;
        
        _enemy.Agent.speed = _speed;
    }

    private void EnterOnAttackRange(Collider2D obj)
    {
        if (!obj.CompareTag("Player") || !obj.GetComponent<Player>()) return;
        Manager.ChangeState(_attackState);
    }

    private void Update()
    {
        _enemy.Agent.SetDestination(_target.position);
    }

    private void OnDisable()
    {
        _attackColEvent.OnEnter -= EnterOnAttackRange;
    }
}
