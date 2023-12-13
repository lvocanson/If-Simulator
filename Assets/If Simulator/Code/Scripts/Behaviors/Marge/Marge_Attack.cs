using System;
using System.Collections;
using System.Collections.Generic;
using Ability;
using UnityEngine;
using FiniteStateMachine;
using SAP2D;
using UnityEngine.Serialization;

public class Marge_Attack : BaseState
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
    
    private Coroutine _attackCoroutine;
    private float _attackTimer;
    bool _isAttacking = false;

    private void OnEnable()
    {
        _attackTimer = _attackDelay + Time.timeSinceLevelLoad;
        _SAPAgent.CanMove = false;
        _SAPAgent.CanSearch = false;
        
        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(Attack());
        }
    }
    
    void Update()
    {
        // Si le joueur est trop loin
        if (!_isAttacking && Vector3.Distance(transform.position, _target.position) > 3f)
        {
            Manager.ChangeState(_chaseState);
        }
    }

    private void MargeShoot()
    {
        Vector3 direction = _target.transform.position - transform.position;
        BulletBehavior bullet = Instantiate(_bullet, transform.position, Quaternion.identity)
            .GetComponent<BulletBehavior>();
        bullet.Initialized(direction, _bulletSpeed,_bulletTimeDestroy);
    }

    private void OnDisable()
    {
        _isAttacking = false; // On a fini d'attaquer
        _SAPAgent.CanMove = true;
        _SAPAgent.CanSearch = true;
        
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }
    
    private IEnumerator Attack()
    {
        while (true)
        {
            MargeShoot();
            _isAttacking = false;
            Debug.Log("ATTACK");
            yield return new WaitForSeconds(_attackDelay);
            _isAttacking = true; 
        }
    }
}
