using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using SAP2D;
using UnityEngine.Serialization;

public class Marge_Chase : BaseState
{
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    
    [Header("State Machine")]
    [SerializeField] private BaseState _patrolState;
    [SerializeField] private BaseState _attackState;
    [SerializeField] SAP2DAgent _SAPAgent;
    
    [Header("Data")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _chaseRange = 2f;
    
    [Header("Events")]
    [SerializeField] private PhysicsEvents _attackColEvent;
    [SerializeField] private PhysicsEvents _chaseColEvent;
    
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
            Manager.ChangeState(_attackState);
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
