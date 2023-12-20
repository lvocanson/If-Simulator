using NaughtyAttributes;
using System.Linq;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// A composite node is a node that contains multiple child nodes.
    /// </summary>
    public abstract class CompositeNodeSo : NodeSo
    {
        /// <summary>
        /// The children of this node.
        /// </summary>
        [field: SerializeField, ReadOnly]
        public NodeSo[] Children { get; set; } = new NodeSo[0];

        public override NodeSo DeepInitialize(Blackboard blackboard)
        {
            var clone = (CompositeNodeSo)base.DeepInitialize(blackboard);
            clone.Children = Children.Select(c => c.DeepInitialize(blackboard)).ToArray();
            return clone;
        }
    }
}
