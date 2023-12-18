using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// A node that logs a message.
    /// </summary>
    public class LoggerSo : ActionNodeSo
    {
        [SerializeField]
        private string _message = null;

        protected override void OnUpdate()
        {
            Debug.Log(_message);
            State = NodeState.Success;
        }
    }
}
