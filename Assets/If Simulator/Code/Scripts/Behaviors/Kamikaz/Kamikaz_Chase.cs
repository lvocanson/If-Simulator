using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using SAP2D;
using UnityEngine.Serialization;

public class Kamikaz_Chase : BaseState
{
    private Transform _target;
    
    [Header("State Machine")]
    [SerializeField] private Kamikaz_Patrol _patrolState;
    [SerializeField] private Kamikaz_Attack _attackState;
    [SerializeField] SAP2DAgent _SAPAgent;
    
    [Header("Data")]
    [SerializeField] private float _speed = 1f;
    
    [Header("Events")]
    [SerializeField] private PhysicsEvents _attackColEvent;
    [SerializeField] private PhysicsEvents _chaseColEvent;

    public void SetTarget(Transform target)
    {
        _target = target;
        _SAPAgent.Target = _target;
    }
    
    private void OnEnable()
    {
        _chaseColEvent.OnExit += ExitOnChaseRange;
        _attackColEvent.OnEnter += EnterOnAttackRange;
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
        _chaseColEvent.OnExit -= ExitOnChaseRange;
        _attackColEvent.OnEnter -= EnterOnAttackRange;
    }
}
