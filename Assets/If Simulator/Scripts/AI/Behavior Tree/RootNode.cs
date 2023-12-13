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
            State = _child?.Evaluate() ?? NodeState.Failure;
        }
    }
}
