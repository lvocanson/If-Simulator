using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// A composite node is a node that contains multiple child nodes.
    /// </summary>
    public abstract class CompositeNode : Node
    {
        /// <summary>
        /// The children of this node.
        /// </summary>
        [field: SerializeField, HideInInspector]
        public Node[] Children { get; set; } = new Node[0];
    }
}
