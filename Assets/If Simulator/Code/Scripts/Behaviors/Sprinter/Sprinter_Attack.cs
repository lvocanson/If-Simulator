using System;
using System.Collections;
using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;

public class Sprinter_Attack : BaseState
{
    [ShowNonSerializedField, Tooltip("The target to move towards")] private Transform _target;
    [Header("References")]
    [SerializeField] private Enemy _enemy;

    [SerializeField] private CircleCollider2D _attackRange; 
    [SerializeField] private BoxCollider2D _hitAttack; 
    
    [Header("State Machine")]
    [SerializeField] private Sprinter_Chase _chaseState;
    
    [Header("Event")]
    [SerializeField] private PhysicsEvents _attackEvent;
    [SerializeField] private PhysicsEvents _damageEvent;
    
    public void SetTarget(Transform target) => _target = target;
    private Coroutine _attackSprinter;


    private void OnEnable()
    {
        _attackEvent.OnExit += ExitAttackRange;
        _damageEvent.OnEnter += EnterDamageZone;
        _damageEvent.OnExit += ExitDamageZone; 
    }

    private void EnterDamageZone(Collider2D obj)
    {
        _enemy.Agent.isStopped = true;
        _attackSprinter ??= StartCoroutine(Attack());
    }
    
    private void ExitDamageZone(Collider2D obj)
    {
        Debug.Log("EXIT RANGE");
        if (_attackSprinter != null)
        {
            StopCoroutine(_attackSprinter);
            _attackSprinter = null;
        }
    }

    private void ExitAttackRange(Collider2D obj)
    {
        Manager.ChangeState(_chaseState);
    }
    
    private void Update()
    {
        transform.up = _target.position - transform.position;
    }

    private IEnumerator Attack()
    {
        while (_target != null)
        {
            Debug.Log("Attack melee");
            _hitAttack.enabled = true;
            yield return new WaitForSeconds(0.1f);
            _hitAttack.enabled = false; 
            yield return new WaitForSeconds(1f);
        }
    }


    private void OnDisable()
    {
        _attackEvent.OnExit -= ExitAttackRange;
        _damageEvent.OnEnter -= EnterDamageZone;
        _damageEvent.OnExit -= ExitDamageZone; 
        _enemy.Agent.isStopped = false;
        
        if (_attackSprinter != null)
        {
            StopCoroutine(_attackSprinter);
            _attackSprinter = null;
        }
    }
}
