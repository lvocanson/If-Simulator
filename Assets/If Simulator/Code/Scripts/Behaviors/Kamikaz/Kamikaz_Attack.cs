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
    [FormerlySerializedAs("_timeToExplode")] [SerializeField] private float _explosionDelay = 2f;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private AnimationCurve _explosionCurve;
    [SerializeField] private float _explosionDuration = 1f;

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Collider2D _collider;
    
    [Header("State Machine")]
    [SerializeField] private BaseState _chaseState;
    [SerializeField] private SAP2DAgent _SAPAgent;
    
    [Header("Event")]
    [SerializeField] private PhysicsEvents _attackEvent;

    private Coroutine _attackCoroutine;
    private GameObject _explosionInstance;

    public void SetTarget(Transform target) => _target = target;

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
        yield return new WaitForSeconds(_explosionDelay);
        //TO DO APPLY DAMAGE TO PLAYER
        
        _explosionInstance = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        
        //Disable Renderer & Collider
        _sprite.enabled = false;
        _collider.enabled = false;
        
        yield return Explode(); 
        IEnumerator Explode()
        {
            float timer = 0;
            while (timer < 1)
            {
                timer += Time.fixedDeltaTime / _explosionDuration;
                float power = _explosionCurve.Evaluate(timer);
                _explosionInstance.transform.localScale = Vector3.one * (power * _explosionRadius);
                yield return new WaitForFixedUpdate(); 
            }
        }
        Destroy(_explosionInstance);
        Destroy(gameObject);
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
