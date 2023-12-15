using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// A behavior tree.
    /// </summary>
    public class BTreeRunner : MonoBehaviour
    {
        [SerializeField, Tooltip("The tree this component will run.")]
        private BTreeSo _tree = null;
        private BTreeSo _instance = null;

        [SerializeField, Tooltip("The blackboard initializer. All fields marked with the SerializeField attribute will be copied to the tree's blackboard.")]
        private MonoBehaviour _blackboardInitializer = null;

        private void Awake()
        {
            if (_tree == null)
            {
                Debug.LogWarning("No tree attached to this component.", this);
                enabled = false;
                return;
            }
            if (!_tree.Validate(out var message))
            {
                Debug.LogError("Tree validation failed: " + message, this);
                enabled = false;
                return;
            }

            _instance = _tree.Clone();
            if (_blackboardInitializer != null)
            {
                var fields = _blackboardInitializer.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(f => f.GetCustomAttribute<SerializeField>() != null);

                foreach (var field in fields)
                {
                    _instance.Blackboard.Write(field.Name, field.GetValue(_blackboardInitializer));
                }
            }
            _instance.Blackboard.Write("GameObject", gameObject);
        }

        private void Update()
        {
            _instance.Update();
        }
    }
}