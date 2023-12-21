using BehaviorTree;
using UnityEngine;

public class MoveForearm : ActionNodeSo
{
    [SerializeField] private float _shoulderAngle;
    [SerializeField] private float _elbowAngle;
    private NebulaForearm _armInstance;

    public override NodeSo DeepInitialize(Blackboard blackboard)
    {
        var clone = (MoveForearm)base.DeepInitialize(blackboard);
        blackboard.Read("Forearm", out clone._armInstance);
        return clone;
    }

    protected override void OnUpdate()
    {
        _armInstance.ShoulderAngle = _shoulderAngle;
        _armInstance.ElbowAngle = _elbowAngle;
        State = NodeState.Success;
    }
}
