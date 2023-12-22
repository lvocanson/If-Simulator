using System;
using System.Collections;
using System.Numerics;
using Ability;
using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;
using Unity.Mathematics;
using Utility;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Marge_Attack : BaseState
{
    [Header("References")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] private CircleCollider2D _attackCol;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject ProjectileSpawnPoint; 
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioSource _audioSource;

    [Header("State Machine")]
    [SerializeField] private Marge_Chase _chaseState;

    [Header("Data")]
    [SerializeField] private float _attackRange = 2;
    [SerializeField] private float _attackDelay = 2f;

    [Header("Bullet Data")]

    [Header("Event")]
    [SerializeField] private PhysicsEvents _attackEvent;

    [Header("Debug")]
    [ShowNonSerializedField] private Transform _target;

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
        if (!obj.CompareTag("Player")) return;

        Manager.ChangeState(_chaseState);
    }

    private void MargeShoot()
    {
        Vector3 direction = _target.transform.position - transform.position;
        Projectile bullet = Instantiate(_bulletPrefab, ProjectileSpawnPoint.transform.position, quaternion.identity)
            .GetComponent<Projectile>();

        if (_shootSound)
        {
            _audioSource.PlayOneShot(_shootSound);
        }
        
        bullet.transform.rotation = Quaternion.AngleAxis(TransformUtility.AngleFromDirection(direction), Vector3.forward);
        bullet.Initialize(gameObject.layer, direction);
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
        _enemy.Agent.isStopped = false;

        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }
}
