using UnityEngine;
using FiniteStateMachine;

public class Sprinter_Attack : BaseState
{
    [SerializeField] private Enemy _enemy;

    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    [SerializeField] private BaseState _previousState;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDelay = 2f;

    private float _attackTimer;
    bool _isAttacking = false;

    private void OnEnable()
    {
        _isAttacking = true; // On est en train d'attaquer
        _attackTimer = _attackDelay + Time.timeSinceLevelLoad;
        _enemy.Agent.isStopped = true;
    }

    void Update()
    {
        if (Time.time > _attackTimer)
        {
            _isAttacking = false;
        }

        // Si le joueur est trop loin
        if (!_isAttacking && Vector3.Distance(transform.position, _target.position) > 1f)
        {
            Manager.ChangeState(_previousState);
        }
    }

    private void OnDisable()
    {
        _isAttacking = false; // On a fini d'attaquer
        _enemy.Agent.isStopped = false;
    }
}
