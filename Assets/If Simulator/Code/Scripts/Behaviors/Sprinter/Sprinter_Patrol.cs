using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;

public class Sprinter_Patrol : BaseState
{
    [Header("References")]
    [SerializeField] private Enemy _enemy;
    [SerializeField] private BaseState _chase;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _playerRange = 2f;
    
    [ShowNonSerializedField] private int _index = 0;
    [ShowNonSerializedField, Tooltip("The target to move towards")] private Transform _currentTarget;


    private void OnEnable()
    {
        _enemy.Agent.SetDestination(_waypoints[_index].position);
        _enemy.Agent.speed = _speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _currentTarget.position) < _playerRange)
        {
            Manager.ChangeState(_chase);
        }

        else if (Vector3.Distance(transform.position, _waypoints[_index].position) < .5f)
        {
            _index = (_index + 1) % _waypoints.Length;
            _enemy.Agent.SetDestination(_waypoints[_index].position);
        }

    }
}
