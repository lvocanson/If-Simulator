using BehaviorTree;

public class ShootShotgun : ActionNodeSo
{
    [UnityEngine.SerializeField] string _shotgunKey = "Shotgun";
    private Shotgun _shotgun;

    public override NodeSo DeepInitialize(Blackboard blackboard)
    {
        var clone = (ShootShotgun)base.DeepInitialize(blackboard);
        blackboard.Read(_shotgunKey, out clone._shotgun);
        return clone;
    }

    protected override void OnUpdate()
    {
        _shotgun.Shoot();
        State = NodeState.Success;
    }
}
