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
        Manager.ChangeState(_chaseState);
    }

    private void TankShoot()
    {
        Vector3 direction = _target.transform.position - transform.position;
        Projectile bullet = Instantiate(_bulletPrefab, ProjectileSpawnPoint.transform.position, quaternion.identity)
            .GetComponent<Projectile>();

        bullet.transform.rotation = Quaternion.AngleAxis(TransformUtility.AngleFromDirection(direction), Vector3.forward);
        bullet.Initialize(gameObject.layer, direction);
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackDelay);
            TankShoot();
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
