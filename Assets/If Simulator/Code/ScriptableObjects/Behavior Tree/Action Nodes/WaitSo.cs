using UnityEngine;

namespace BehaviorTree
{
    public class WaitSo : ActionNodeSo
    {
        [SerializeField, Tooltip("The time to wait in seconds.")]
        private float _time = 1f;

        private float _startTime;

        protected override void OnEnter()
        {
            _startTime = Time.time;
        }

        protected override void OnUpdate()
        {
            if (Time.time - _startTime >= _time)
            {
                State = NodeState.Success;
            }
        }
    }
}
