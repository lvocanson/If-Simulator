using System;
using System.Collections;
using System.Collections.Generic;
using Ability;
using UnityEngine;
using FiniteStateMachine;
using SAP2D;
using UnityEngine.Serialization;

public class Kamikaz_Attack : BaseState
{
    [Header("Data")]
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDelay = 2f;
    
    [Header("State Machine")]
    [SerializeField] private BaseState _chaseState;
    [SerializeField] private SAP2DAgent _SAPAgent;
    
    [Header("Bullet Data")]
    [SerializeField] private float _bulletSpeed = 2f;
    [SerializeField] private float _bulletTimeDestroy = 2f;
    [SerializeField] GameObject _bullet;
    
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

    private void ExitAttackRange(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
            Manager.ChangeState(_chaseState);
    }    

    private void MargeShoot()
    {
        Vector3 direction = _target.transform.position - transform.position;
        BulletBehavior bullet = Instantiate(_bullet, transform.position, Quaternion.identity)
            .GetComponent<BulletBehavior>();
        bullet.Initialized(direction, _bulletSpeed,_bulletTimeDestroy);
    }
    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackDelay);
            MargeShoot();
        }
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
