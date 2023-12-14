using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Ability;
using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;
using SAP2D;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class Kamikaz_Attack : BaseState
{
    [Header("Data")]
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    [SerializeField] private int _timeToExplode = 2;
    [SerializeField] private GameObject _explosion; 
    
    [Header("State Machine")]
    [SerializeField] private BaseState _chaseState;
    [SerializeField] private SAP2DAgent _SAPAgent;
    
    [Header("Event")]
    [SerializeField] private PhysicsEvents _attackEvent;

    private Coroutine _attackCoroutine;

    

    private void OnEnable()
    {
        _attackEvent.OnExit += ExitAttackRange;
        _SAPAgent.CanMove = false;
        _SAPAgent.CanSearch = false;
        
        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(_timeToExplode);
        
        //TO DO APPLY DAMAGE TO PLAYER
        Destroy(gameObject);
        Instantiate(_explosion, transform.position, Quaternion.identity);
    }

    private void ExitAttackRange(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
            Manager.ChangeState(_chaseState);
    }    
    

    private void OnDisable()
    {
        _attackEvent.OnExit -= ExitAttackRange;
        _SAPAgent.CanMove = true;
        _SAPAgent.CanSearch = true;
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }
    
}
