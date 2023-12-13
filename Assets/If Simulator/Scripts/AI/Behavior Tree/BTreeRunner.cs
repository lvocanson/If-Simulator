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
        private BTree _tree = null;

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

            if (_blackboardInitializer != null)
            {
                var fields = _blackboardInitializer.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(f => f.GetCustomAttribute<SerializeField>() != null);

                foreach (var field in fields)
                {
                    _tree.Blackboard.Write(field.Name, field.GetValue(_blackboardInitializer));
                }
            }

            _tree.Blackboard.Write("GameObject", gameObject);
        }

        private void Update()
        {
            _tree.Update();
        }
    }
}
