namespace BehaviorTree
{
    /// <summary>
    /// A decorator node that inverts the result of its child.
    /// </summary>
    public class Inverter : Decorator
    {
        public Inverter(BTree tree, Node child) : base(tree, child)
        {
        }

        public override NodeState Evaluate()
        {
            State = Child.Evaluate() switch
            {
                NodeState.Running => NodeState.Running,
                NodeState.Failure => NodeState.Success,
                _ => NodeState.Failure
            };
            return State;
        }
    }
}
