namespace BehaviorTree
{
    /// <summary>
    /// An inverter node returns the opposite of its child.
    /// </summary>
    public class InverterSo : DecoratorNodeSo
    {
        protected override void OnUpdate()
        {
            State = Child.Evaluate() switch
            {
                NodeState.Failure => NodeState.Success,
                NodeState.Success => NodeState.Failure,
                _ => NodeState.Running,
            };
        }
    }
}
