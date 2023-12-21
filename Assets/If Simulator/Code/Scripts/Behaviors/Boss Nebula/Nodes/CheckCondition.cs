using BehaviorTree;
using UnityEngine;

public class CheckCondition : ActionNodeSo
{
    [SerializeField] private string _conditionName;
    [SerializeField] private bool _defaultValue;

    public override NodeSo DeepInitialize(Blackboard blackboard)
    {
        if (!blackboard.Contains(_conditionName))
            blackboard.Write(_conditionName, _defaultValue);

        return base.DeepInitialize(blackboard);
    }

    protected override void OnUpdate()
    {
        Blackboard.Read(_conditionName, out bool c);
        State = c ? NodeState.Success : NodeState.Failure;
    }
}
