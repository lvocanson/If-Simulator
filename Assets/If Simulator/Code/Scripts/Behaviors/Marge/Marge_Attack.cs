using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using SAP2D;
using UnityEngine.Serialization;

public class Marge_Attack : BaseState
{
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    [SerializeField] private BaseState _previousState;
    [SerializeField] private float _attackRange;
    [SerializeField] private SAP2DAgent _SAPAgent;
    [SerializeField] private float _attackDelay = 2f;

    public GameObject _bullet;
    
    private Coroutine _attackCoroutine;
    private float _attackTimer;
    bool _isAttacking = false;

    private void OnEnable()
    {
        _isAttacking = true; // On est en train d'attaquer
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
            Manager.ChangeState(_previousState);
        }
    }

    private void MargeShoot()
    {
        _isAttacking = true;
        Instantiate(_bullet, transform.position, Quaternion.identity);
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
        while (_isAttacking)
        {
            MargeShoot();
            yield return new WaitForSeconds(_attackDelay);
            _isAttacking = false;
        }
    }
}
