using BehaviorTree;
using UnityEngine;

public class RandomDestinationTank : ActionNodeSo
{
    [SerializeField] private Vector2 _min;
    [SerializeField] private Vector2 _max;

    protected override void OnUpdate()
    {
        Vector2 destination = new(Random.Range(_min.x, _max.x), Random.Range(_min.y, _max.y));
        Blackboard.Write("Destination", destination);
        State = NodeState.Success;
    }
}
