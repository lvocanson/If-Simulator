namespace BehaviorTree
{
    /// <summary>
    /// A selector node is a node that will evaluate its children in order until one succeeds.
    /// </summary>
    public class Selector : Node
    {
        /// <summary>
        /// Creates a selector node (until one succeeds).
        /// </summary>
        public Selector(Tree tree, params Node[] children) : base(tree, children)
        {
        }

        public override NodeState Evaluate()
        {
            foreach (Node child in Children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Success:
                        State = NodeState.Success;
                        return State;
                    case NodeState.Running:
                        State = NodeState.Running;
                        return State;
                    default:
                        continue;
                }
            }

            State = NodeState.Failure;
            return State;
        }
    }
}
