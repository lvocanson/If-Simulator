using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// A behavior tree.
    /// </summary>
    public class BTree : MonoBehaviour
    {
        [SerializeField, Tooltip("The tree this component will execute.")]
        private BTreeAsset _treeAsset = null;

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
            if (_treeAsset == null)
            {
                Debug.LogWarning("No tree attached to this component.", this);
                enabled = false;
                return;
            }

            Root = _treeAsset.CreateTree(this);
        }

        private void Update()
        {
            Root.Evaluate();
        }
    }
}
