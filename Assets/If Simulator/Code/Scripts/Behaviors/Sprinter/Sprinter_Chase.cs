using UnityEngine;
using FiniteStateMachine;
using SAP2D;
using UnityEngine.Serialization;

public class Sprinter_Chase : BaseState
{
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    [FormerlySerializedAs("_previousState"), SerializeField] private BaseState _patrolState;
    [FormerlySerializedAs("_nextState"), SerializeField] private BaseState _attackState;
    [SerializeField] SAP2DAgent _SAPAgent;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _chaseRange = 2f;
    [SerializeField] private float _attackRange = 0.1f;

    private void OnEnable()
    {
        Debug.Log("Focus : " + _target.name + " OnEnabled.");
        _SAPAgent.Target = _target;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, _target.position) > _chaseRange)
        {
            Manager.ChangeState(_patrolState);
        }
        else if (Vector3.Distance(transform.position, _target.position) < _attackRange)
        {
            Manager.ChangeState(_attackState);
        }
    }
}
