using BehaviorTree;
using UnityEngine;

public class EnemyBehavior : BTree
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private Transform _base;
    [SerializeField] private float _speed;

    protected override Node CreateTree()
    {
        // Write to the blackboard
        Blackboard.Write("Speed", _speed);

        // Create the tree
        return new Selector(this,
            new Sequence(this,
                new GoBaseCondition(this),
                new GoBaseAction(this, _base)
            ),
            new PatrollingAction(this, _waypoints)
        );
    }
}
