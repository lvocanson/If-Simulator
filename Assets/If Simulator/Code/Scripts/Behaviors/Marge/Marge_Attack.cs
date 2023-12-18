using System.Collections;
using Ability;
using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;

public class Marge_Attack : BaseState
{
    [Header("References")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] private CircleCollider2D _attackCol;
    [SerializeField] private Enemy _enemy;

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

    private void ExitAttackRange(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
            Manager.ChangeState(_chaseState);
    }

    private void MargeShoot()
    {
        Vector3 direction = _target.transform.position - transform.position;
        Projectile bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity)
            .GetComponent<Projectile>();
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
