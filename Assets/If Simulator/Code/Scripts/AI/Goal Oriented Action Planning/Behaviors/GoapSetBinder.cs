using CrashKonijn.Goap.Behaviours;
using UnityEngine;

namespace IfSimulator.GOAP.Behaviors
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class GoapSetBinder : MonoBehaviour
    {
        [SerializeField] private GoapRunnerBehaviour GoapRunner;
        
        
        private void Awake()
        {
            if (!GoapRunner) GoapRunner = FindFirstObjectByType<GoapRunnerBehaviour>();
            
            AgentBehaviour agent = GetComponent<AgentBehaviour>();
            agent.GoapSet = GoapRunner.GetGoapSet("AllySet");
        }
    }
}