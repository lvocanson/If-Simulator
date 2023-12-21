using BehaviorTree;

public class ShootBig : ActionNodeSo
{
    private Shooter _shooter;

    public override NodeSo DeepInitialize(Blackboard blackboard)
    {
        var clone = (ShootBig)base.DeepInitialize(blackboard);
        blackboard.Read("BigShooter", out clone._shooter);
        return clone;
    }

    protected override void OnUpdate()
    {
        State = _shooter.Shoot() ? NodeState.Success : NodeState.Failure;
    }
}
