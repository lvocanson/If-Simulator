using UnityEngine;
using UnityEngine.AI;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;

namespace IfSimulator.GOAP.Behaviors
{
    [RequireComponent(typeof(NavMeshAgent), typeof(AgentBehaviour))]
    public class AgentMoveBehavior : MonoBehaviour
    {
        private NavMeshAgent Agent;
        private AgentBehaviour AgentBehaviour;
        private ITarget CurrentTarget;
        [SerializeField] private float MinMoveDistance = 3f;
        private Vector3 LastPosition;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;
            Agent.stoppingDistance = MinMoveDistance; 
            AgentBehaviour = GetComponent<AgentBehaviour>();
        }

        private void OnEnable()
        {
            AgentBehaviour.Events.OnTargetChanged += EventsOnTargetChanged;
            AgentBehaviour.Events.OnTargetOutOfRange += EventsOnTargetOutOfRange;
        }

        private void OnDisable()
        {
            AgentBehaviour.Events.OnTargetChanged -= EventsOnTargetChanged;
            AgentBehaviour.Events.OnTargetOutOfRange -= EventsOnTargetOutOfRange;
        }

        private void EventsOnTargetChanged(ITarget target, bool inRange)
        {
            CurrentTarget = target;
            LastPosition = CurrentTarget.Position;
            Agent.SetDestination(target.Position);
        }

        private void EventsOnTargetOutOfRange(ITarget target)
        {
        }

        private void Update()
        {
            if (CurrentTarget == null)
            {
                return;
            }

            if (MinMoveDistance <= Vector3.Distance(CurrentTarget.Position, LastPosition))
            {
                LastPosition = CurrentTarget.Position;
                Agent.SetDestination(CurrentTarget.Position);
            }
        }
    }
}
