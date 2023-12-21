using UnityEngine;
using BehaviorTree;

public class AttackPhase1 : ActionNodeSo
{
    private Transform _transform;
    private Transform[] _waypoints;
    private int _currentWaypointIndex = 0;

    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;

    public AttackPhase1(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;
    }
    protected override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
