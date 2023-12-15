using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// The root node of a tree.
    /// </summary>
    public class RootNode : NodeSo
    {
        [field: SerializeField, HideInInspector]
        public NodeSo Child { get; set; }

        public override NodeSo DeepInitialize(Blackboard blackboard)
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
