namespace BehaviorTree
{
    /// <summary>
    /// A composite node is a node that contains multiple child nodes.
    /// </summary>
    [System.Serializable]
    public abstract class CompositeNode : Node
    {
        [UnityEngine.SerializeReference]
        private readonly Node[] _children = new Node[0];

        /// <summary>
        /// The children of this node.
        /// </summary>
        public Node[] Children => _children;
    }
}
