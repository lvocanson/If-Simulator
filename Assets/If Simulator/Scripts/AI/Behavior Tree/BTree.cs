using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(fileName = "New BTree", menuName = "Scriptable Objects/Behavior Tree")]
    public class BTree : ScriptableObject
    {
        [SerializeReference]
        private readonly RootNode _root;

        /// <summary>
        /// The tree's blackboard.
        /// </summary>
        public Blackboard Blackboard { get; } = new();

        /// <summary>
        /// State of the tree.
        /// </summary>
        public NodeState State => _root.State;

        public BTree()
        {
            _root = new RootNode();
        }

        public void Update()
        {
            if (State == NodeState.Running)
                _root.Evaluate();
        }
    }
}
