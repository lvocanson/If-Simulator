namespace BehaviorTree
{
    public enum NodeState
    {
        Success, // The node has finished running and succeeded
        Failure, // The node has finished running and failed
        Running, // The node is still running (Needs to be evaluated again)
    }

    public abstract class Node
    {
        /// <summary>
        /// The state of the last evaluation of the node.
        /// </summary>
        public NodeState State { get; protected set; } = NodeState.Running;

        /// <summary>
        /// The parent of the node.
        /// </summary>
        public Node Parent { get; private set; } = null;

        /// <summary>
        /// The children of the node.
        /// </summary>
        public Node[] Children { get; }

        private readonly BTree _tree;
        /// <summary>
        /// Gets the tree's blackboard.
        /// </summary>
        protected virtual Blackboard Blackboard => _tree.Blackboard;

        /// <summary>
        /// Creates a node attached to a tree with the given children.
        /// </summary>
        public Node(BTree tree, params Node[] children)
        {
            _tree = tree;
            Children = children;
            foreach (Node child in Children)
            {
                child.Parent = this;
            }
        }

        /// <summary>
        /// Evaluates the node and returns its state.
        /// </summary>
        public abstract NodeState Evaluate();

        /// <summary>
        /// Resets the node to its initial state.
        /// </summary>
        public virtual void Reset()
        {
            State = NodeState.Running;
            foreach (Node child in Children)
            {
                child.Reset();
            }
        }
    }
}
