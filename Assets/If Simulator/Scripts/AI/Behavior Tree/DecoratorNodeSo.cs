using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// A decorator node is a node that contains a single child node.
    /// </summary>
    public abstract class DecoratorNodeSo : NodeSo
    {
        /// <summary>
        /// The child of this node.
        /// </summary>
        [field: SerializeField, HideInInspector]
        public NodeSo Child { get; set; }

        public override NodeSo DeepInitialize(Blackboard blackboard)
        {
            var clone = (DecoratorNodeSo)base.DeepInitialize(blackboard);
            clone.Child = Child.DeepInitialize(blackboard);
            return clone;
        }
    }
}
