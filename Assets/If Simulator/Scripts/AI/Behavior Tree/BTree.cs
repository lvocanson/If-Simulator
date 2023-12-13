using System.Linq;
using System.Reflection;
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

        [SerializeField, Tooltip("The blackboard initializer. All fields marked with the SerializeField attribute will be copied to the tree's blackboard.")]
        private MonoBehaviour _blackboardInitializer = null;

        /// <summary>
        /// The tree's root node.
        /// </summary>
        public Node Root { get; private set; } = null;

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

            if (_blackboardInitializer != null)
            {
                var fields = _blackboardInitializer.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(f => f.GetCustomAttribute<SerializeField>() != null);

                foreach (var field in fields)
                {
                    Blackboard.Write(field.Name, field.GetValue(_blackboardInitializer));
                }
            }

            Root = _treeAsset.CreateTree(this);
        }

        private void Update()
        {
            Root.Evaluate();
        }
    }
}
