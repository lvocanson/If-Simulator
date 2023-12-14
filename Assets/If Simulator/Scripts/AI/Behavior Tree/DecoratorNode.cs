using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// A decorator node is a node that contains a single child node.
    /// </summary>
    public abstract class DecoratorNode : Node
    {
        /// <summary>
        /// The child of this node.
        /// </summary>
        [field: SerializeField, HideInInspector]
        public Node Child { get; set; }

        public override Node DeepInitialize(Blackboard blackboard)
        {
            var clone = (DecoratorNode)base.DeepInitialize(blackboard);
            clone.Child = Child.DeepInitialize(blackboard);
            return clone;
        }
    }
}
