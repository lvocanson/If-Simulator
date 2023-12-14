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

        protected override void OnUpdate()
        {
            State = Child.Evaluate();
        }
    }
}
