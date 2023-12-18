using UnityEngine;
using FiniteStateMachine;
using SAP2D;

public class Sprinter_Attack : BaseState
{
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    [SerializeField] private BaseState _previousState;
    [SerializeField] private float _attackRange;
    [SerializeField] private SAP2DAgent _SAPAgent;
    [SerializeField] private float _attackDelay = 2f;

    private float _attackTimer;
    bool _isAttacking = false;

    private void OnEnable()
    {
        _isAttacking = true; // On est en train d'attaquer
        _attackTimer = _attackDelay + Time.timeSinceLevelLoad;
        _SAPAgent.CanMove = false;
        _SAPAgent.CanSearch = false;
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
        _SAPAgent.CanMove = true;
        _SAPAgent.CanSearch = true;
    }
}
