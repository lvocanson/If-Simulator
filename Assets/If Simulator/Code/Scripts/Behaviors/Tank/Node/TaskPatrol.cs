using BehaviorTree;
using UnityEngine;

public class TaskPatrol : ActionNodeSo
{
    private Transform _transform;
    private Transform[] _waypoints;

    private int _currentWaypointIndex = 0;

    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;

    private float _speed = 2f;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
    }

    protected override void OnUpdate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
                _waitCounter = 0f;
            }
        }
        else
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _waypoints[_currentWaypointIndex].position, _speed * Time.deltaTime);
            if (Vector3.Distance(_transform.position, _waypoints[_currentWaypointIndex].position) < 0.1f)
            {
                _waiting = true;
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            }
        }

        State = NodeState.Success;
    }
}
