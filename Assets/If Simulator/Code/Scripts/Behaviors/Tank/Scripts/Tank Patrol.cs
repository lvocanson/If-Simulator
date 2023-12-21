using UnityEngine;
using BehaviorTree;

public class Tank_Patrol : ActionNodeSo
{
    private PhysicsEvents _chaseColEvent;
    private Enemy _enemy;
    private Transform[] _waypoints;
    private int _index = 0;
    private float _speed = 1f;

    private void OnEnable()
    {
        Blackboard.Read("chaseColEvent", out _chaseColEvent);
        Blackboard.Read("enemy", out _enemy);
        Blackboard.Read("waypoints", out _waypoints);

        _chaseColEvent.OnEnter += OnPlayerEnteredRange;
        _enemy.Agent.SetDestination(_waypoints[_index].position);
        _enemy.Agent.speed = _speed;
    }

    private void OnPlayerEnteredRange(Collider2D collider2D)
    {
        Blackboard.Write("CanChase", true);
        Blackboard.Write("Target", collider2D.transform);
        Blackboard.Read("Target", out Transform target);
    }

    protected override void OnUpdate()
    {
        if (_waypoints.Length > 0 && Vector3.Distance(_enemy.transform.position, _waypoints[_index].position) > 0.5f) 
        {
            _index = (_index + 1) % _waypoints.Length;
            _enemy.Agent.SetDestination(_waypoints[_index].position);
        }
    }

    private void OnDisable()
    {
        _chaseColEvent.OnEnter -= OnPlayerEnteredRange;
    }
}
