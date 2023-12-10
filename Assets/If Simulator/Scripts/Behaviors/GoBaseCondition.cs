using BehaviorTree;
using UnityEngine;

public class GoBaseCondition : Node
{
    bool _orderGiven = false;

    public GoBaseCondition(Tree tree) : base(tree)
    {
    }

    public override NodeState Evaluate()
    {
        _orderGiven ^= Input.GetKeyDown(KeyCode.Space);

        State = _orderGiven ? NodeState.Success : NodeState.Failure;
        return State;
    }
}
