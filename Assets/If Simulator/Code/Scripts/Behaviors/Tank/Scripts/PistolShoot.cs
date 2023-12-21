using UnityEngine;
using BehaviorTree;

public class PistolShoot : ActionNodeSo
{
    private Pistol _pistol;

    public override NodeSo DeepInitialize(Blackboard blackboard)
    {
        var clone = (PistolShoot)base.DeepInitialize(blackboard);
        blackboard.Read("Pistol", out clone._pistol);
        return clone;
    }

    protected override void OnUpdate()
    {
        _pistol.Shoot();
        State = NodeState.Success;
    }
}
