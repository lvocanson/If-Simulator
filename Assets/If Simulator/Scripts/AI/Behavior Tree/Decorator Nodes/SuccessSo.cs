namespace BehaviorTree
{
    /// <summary>
    /// A success node always returns success or running.
    /// </summary>
    public class SuccessSo : DecoratorNodeSo
    {
        protected override void OnUpdate()
        {
            State = Child.Evaluate();
            if (State == NodeState.Failure)
                State = NodeState.Success;
        }
    }
}
