using BehaviorTree;
using UnityEngine;

public class GoBaseAction : Node
{
    private readonly Transform _transform;
    private readonly Transform _base;
    private readonly float _speed;

    public GoBaseAction(BTreeRunner tree) : base(tree)
    {
        _transform = tree.transform;
        Blackboard.Read("Base", out _base);
        Blackboard.Read("Speed", out _speed);
    }

    public override NodeState Evaluate()
    {
        if (Vector3.Distance(_transform.position, _base.position) < 0.1f)
        {
            State = NodeState.Success;
            return State;
        }

        _transform.position = Vector3.MoveTowards(_transform.position, _base.position, _speed * Time.deltaTime);
        State = NodeState.Running;
        return State;
    }
}
