using CrashKonijn.Goap.Behaviours;
using IfSimulator.GOAP.Goals;
using UnityEngine;

namespace IfSimulator.GOAP.Behaviors
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class AllyBrain : MonoBehaviour
    {
        private AgentBehaviour AgentBehaviour;

        private void Awake()
        {
            AgentBehaviour = GetComponent<AgentBehaviour>();
        }

        private void Start()
        {
            AgentBehaviour.SetGoal<WanderGoal>(false);
        }
    }

}
