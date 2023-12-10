namespace BehaviorTree
{
    /// <summary>
    /// A sequence node is a node that will evaluate its children in order until one fails.
    /// </summary>
    public class Sequence : Node
    {
        /// <summary>
        /// Creates a sequence node (until one fails).
        /// </summary>
        public Sequence(Tree tree, params Node[] children) : base(tree, children)
        {
        }

        public override NodeState Evaluate()
        {
            foreach (Node child in Children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Failure:
                        State = NodeState.Failure;
                        return State;
                    case NodeState.Running:
                        State = NodeState.Running;
                        return State;
                    default:
                        continue;
                }
            }

            State = NodeState.Success;
            return State;
        }
    }
}
