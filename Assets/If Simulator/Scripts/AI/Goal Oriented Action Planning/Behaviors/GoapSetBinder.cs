using CrashKonijn.Goap.Behaviours;
using UnityEngine;

namespace CrashKonijn.Goap.Behaviors
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class GoapSetBinder : MonoBehaviour
    {
        [SerializeField] private GoapRunnerBehaviour GoapRunner;
        private AgentBehaviour AgentBehaviour;

        private void Awake()
        {
            AgentBehaviour = GetComponent<AgentBehaviour>();
            AgentBehaviour.GoapSet = GoapRunner.GetGoapSet("AllySet");
        }
    }

}
