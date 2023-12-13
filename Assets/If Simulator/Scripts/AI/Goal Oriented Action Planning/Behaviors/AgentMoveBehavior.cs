using UnityEngine;
using SAP2D;
using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Interfaces;

namespace IfSimulator.GOAP.Behaviors
{
    // TODO : ADD Animator in futur
    [RequireComponent(typeof(SAP2DAgent), typeof(AgentBehaviour))]
    public class AgentMoveBehavior : MonoBehaviour
    {
        private SAP2DAgent Agent;
        private AgentBehaviour AgentBehaviour;
        private ITarget CurrentTarget;
        [SerializeField] private float MinMoveDistance = 0.25f;
        private Vector3 LastPosition;
        private void Awake()
        {
            Agent = GetComponent<SAP2DAgent>();
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
            Agent.transform.position = target.Position;
        }

        private void EventsOnTargetOutOfRange(ITarget target)
        {

        }

        private void Update()
        {
            if (CurrentTarget != null)
            {
                return;
            }

            if (MinMoveDistance <= Vector3.Distance(CurrentTarget.Position, LastPosition)) 
            {
                LastPosition = CurrentTarget.Position;
                Agent.transform.position = CurrentTarget.Position;
            }
        }
    }

}
