using BehaviorTree;

public class Shoot : ActionNodeSo
{
    private Shooter _shooter;

    public override NodeSo DeepInitialize(Blackboard blackboard)
    {
        var clone = (Shoot)base.DeepInitialize(blackboard);
        blackboard.Read("Shooter", out clone._shooter);
        return clone;
    }

    protected override void OnUpdate()
    {
        State = _shooter.Shoot() ? NodeState.Success : NodeState.Failure;
    }
}
