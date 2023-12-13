namespace BehaviorTree
{
    /// <summary>
    /// A failure node always returns failure or running.
    /// </summary>
    public class Failure : DecoratorNode
    {
        protected override void OnUpdate()
        {
            State = Child.Evaluate();
            if (State == NodeState.Success)
                State = NodeState.Failure;
        }
    }
}
