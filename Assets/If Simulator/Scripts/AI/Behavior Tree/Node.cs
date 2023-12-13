using System;

namespace BehaviorTree
{
    public enum NodeState
    {
        Success, // The node has finished running and succeeded
        Failure, // The node has finished running and failed
        Running, // The node is still running (Needs to be evaluated again)
    }

    /// <summary>
    /// Base class for all nodes of a tree.
    /// </summary>
    /// <remarks>
    /// Your derived classes must use the following constructor:<br />
    /// <c>T(BTree tree, params Node[] children) : base(tree, children)</c>
    /// </remarks>
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

        private readonly BTreeRunner _tree;
        /// <summary>
        /// Gets the tree's blackboard.
        /// </summary>
        protected virtual Blackboard Blackboard => _tree.Blackboard;

        /// <summary>
        /// Creates a node of the given type attached to a tree with the given children.
        /// </summary>
        public static Node Create(string typeName, BTreeRunner tree, params Node[] children)
        {
            Type type = Type.GetType(typeName) ?? throw new ArgumentException(
                $"Type {typeName} not found. Possible causes:\n" +
                $"- The type is not in the same assembly as the Node class.\n" +
                $"- You forgot to add the namespace to the type name.\n" +
                $"- The type name is misspelled.\n");

            if (!type.IsSubclassOf(typeof(Node)))
                throw new ArgumentException($"Type {typeName} is not a subclass of Node.");

            var args = children == null || children.Length == 0
                ? new object[] { tree }
                : new object[] { tree, children };

            return (Node)Activator.CreateInstance(type, args);
        }

        /// <summary>
        /// Creates a node attached to a tree with the given children.
        /// </summary>
        public Node(BTreeRunner tree, params Node[] children)
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
