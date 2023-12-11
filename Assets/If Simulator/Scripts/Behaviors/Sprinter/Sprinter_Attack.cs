using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;

public class Sprinter_Attack : BaseState
{
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    [SerializeField] private BaseState _nextState;
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private float _attackDamage = 1f;
    [SerializeField] private float _attackCooldown = 1f;

    private void OnEnable()
    {
        //throw new NotImplementedException();
    }
    void Update()
    {
        // Si plus in range
        Manager.ChangeState(_nextState);
    }

    private void OnDisable()
    {
        //throw new NotImplementedException();
    }
}
