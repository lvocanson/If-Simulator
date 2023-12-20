using BehaviorTree;
using UnityEngine;

public class SetAgentDestination : ActionNodeSo
{
    [SerializeField] private Vector3Event _onDestinationChanged = null;

    protected override void OnUpdate()
    {
        Blackboard.Read("Destination", out Vector2 destination);
        _onDestinationChanged.Raise(destination);
    }
}
