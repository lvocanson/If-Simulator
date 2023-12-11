using BehaviorTree;
using UnityEngine;

public class PatrollingAction : Node
{
    private readonly Transform _transform;
    private readonly Transform[] _waypoints;
    private readonly float _speed;
    private int _currentWaypoint = 0;

    public PatrollingAction(BTree tree, Transform[] waypoints) : base(tree)
    {
        _transform = tree.transform;
        _waypoints = waypoints;
        _speed = Blackboard.Read<float>("Speed");

        State = NodeState.Running;
    }

    public override NodeState Evaluate()
    {
        if (Vector3.Distance(_transform.position, _waypoints[_currentWaypoint].position) < 0.1f)
        {
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
        }

        _transform.position = Vector3.MoveTowards(_transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
        return State;
    }
}
