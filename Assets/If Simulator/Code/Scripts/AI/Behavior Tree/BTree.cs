using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// A behavior tree.
    /// </summary>
    public abstract class BTree : MonoBehaviour
    {
        /// <summary>
        /// The tree's root node.
        /// </summary>
        public Node Root {  get; private set; } = null;

        /// <summary>
        /// The tree's blackboard.
        /// </summary>
        public Blackboard Blackboard { get; } = new();

        private void Awake()
        {
            Root = CreateTree();
        }

        private void Update()
        {
            Root.Evaluate();
        }

        /// <summary>
        /// Create the tree. This method is called in Awake.
        /// </summary>
        /// <returns>The tree's root node.</returns>
        protected abstract Node CreateTree();
    }
}
