using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// The root node of a tree.
    /// </summary>
    [System.Serializable]
    public class RootNode : Node
    {
        [SerializeReference]
        private Node _child = null;

        protected override void OnUpdate()
        {
            if (_child == null)
            {
                State = NodeState.Failure;
                return;
            }

            State = _child.Evaluate();
        }
    }
}
