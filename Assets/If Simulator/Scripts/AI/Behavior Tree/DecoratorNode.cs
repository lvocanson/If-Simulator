namespace BehaviorTree
{
    /// <summary>
    /// A decorator node is a node that contains a single child node.
    /// </summary>
    [System.Serializable]
    public abstract class DecoratorNode : Node
    {
        [UnityEngine.SerializeReference]
        private readonly Node _child;

        /// <summary>
        /// The child of this node.
        /// </summary>
        public Node Child => _child;
    }
}
