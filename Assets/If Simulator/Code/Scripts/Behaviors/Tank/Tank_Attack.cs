using System;
using System.Collections;
using Ability;
using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;
using Unity.Mathematics;
using Utility;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Tank_Attack : BaseState
{
    [Header("References")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] private CircleCollider2D _attackCol;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject ProjectileSpawnPoint;

    [Header("State Machine")]
    [SerializeField] private Tank_Chase _chaseState;

    [Header("Data")]
    [SerializeField] private float _attackRange = 5;
    [SerializeField] private float _attackDelay = 2f;
    [SerializeField] private int _attackBurst = 3;
    [SerializeField] private float _delayBetweenBurst = .5f;

    [Header("Event")]
    [SerializeField] private PhysicsEvents _attackEvent;

    [Header("Debug")]
    [ShowNonSerializedField] private Transform _target;
    
    private bool _isAttacking;
    private Coroutine _attackCoroutine;

    public void SetTarget(Transform target) => _target = target;

    private void Awake()
    {
        _attackCol.radius = _attackRange;
    }

    private void OnEnable()
    {
        _attackEvent.OnExit += ExitAttackRange;
        _enemy.Agent.isStopped = true;

        _attackCoroutine ??= StartCoroutine(Attack());
    }

    private void Update()
    {
        transform.up = _target.position - transform.position;
    }

    private void ExitAttackRange(Collider2D obj)
    {
        if (!obj.CompareTag("Player") || !obj.GetComponent<Player>()) return;

        Manager.ChangeState(_chaseState);
    }

    private void Shoot()
    {
        Vector3 direction = _target.transform.position - transform.position;
        Projectile bullet = Instantiate(_bulletPrefab, ProjectileSpawnPoint.transform.position, quaternion.identity)
            .GetComponent<Projectile>();

        bullet.transform.rotation = Quaternion.AngleAxis(TransformUtility.AngleFromDirection(direction), Vector3.forward);
        bullet.Initialize(gameObject.layer, direction);
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        
        while (_isAttacking)
        {
            yield return new WaitForSeconds(_attackDelay);
            for (int i = 0; i < _attackBurst; i++)
            {
                Shoot();
                yield return new WaitForSeconds(_delayBetweenBurst);
            }
        }
        
        _attackCoroutine = null;
    }

    private void OnDisable()
    {
        _attackEvent.OnExit -= ExitAttackRange;
        _enemy.Agent.isStopped = false;
        _isAttacking = false;
    }

    private void OnDestroy()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }
}
