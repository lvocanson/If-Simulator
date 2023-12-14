namespace BehaviorTree
{
    /// <summary>
    /// A decorator node is a node that contains a single child node. It is used to modify the behaviour of the child node.
    /// </summary>
    public abstract class Decorator : Node
    {
        /// <summary>
        /// The child node of the decorator node.
        /// </summary>
        protected Node Child { get; }

        public Decorator(BTree tree, Node child) : base(tree, child)
        {
            Child = child;
        }
    }
}
