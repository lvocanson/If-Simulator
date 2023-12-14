using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// The root node of a tree.
    /// </summary>
    public class RootNode : Node
    {
        [field: SerializeField, HideInInspector]
        public Node Child { get; set; }

        public override Node DeepInitialize(Blackboard blackboard)
        {
            var clone = (RootNode)base.DeepInitialize(blackboard);
            clone.Child = Child.DeepInitialize(blackboard);
            return clone;
        }

        protected override void OnUpdate()
        {
            State = Child.Evaluate();
        }
    }
}
