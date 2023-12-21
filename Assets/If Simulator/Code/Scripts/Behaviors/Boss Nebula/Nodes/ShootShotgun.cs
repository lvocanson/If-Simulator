using BehaviorTree;

public class ShootShotgun : ActionNodeSo
{
    private Shotgun _shotgun;

    public override NodeSo DeepInitialize(Blackboard blackboard)
    {
        var clone = (ShootShotgun)base.DeepInitialize(blackboard);
        blackboard.Read("Shotgun", out clone._shotgun);
        return clone;
    }

    protected override void OnUpdate()
    {
        _shotgun.Shoot();
        State = NodeState.Success;
    }
}
