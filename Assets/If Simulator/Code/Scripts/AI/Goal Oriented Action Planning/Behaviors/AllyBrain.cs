using CrashKonijn.Goap.Behaviours;
using IfSimulator.GOAP.Config;
using IfSimulator.GOAP.Goals;
using IfSimulator.GOAP.Sensors;
using UnityEngine;

namespace IfSimulator.GOAP.Behaviors
{
    [RequireComponent(typeof(AgentBehaviour))]
    public class AllyBrain : MonoBehaviour
    {
        //[SerializeField] private PlayerSensor PlayerSensor;
        //[SerializeField] private AttackConfigSO AttackConfig;
        private AgentBehaviour AgentBehaviour;

        private void Awake()
        {
            AgentBehaviour = GetComponent<AgentBehaviour>();
        }
        private void Start()
        {
            AgentBehaviour.SetGoal<WanderGoal>(false);
            //PlayerSensor.Collider.radius = AttackConfig.SensorRadius;
        }

        //private void OnEnable()
        //{
        //    PlayerSensor.OnPlayerEnter += PlayerSensorOnPlayerEnter;
        //    PlayerSensor.OnPlayerExit += PlayerSensorOnPlayerExit;
        //}

        //private void OnDisable()
        //{
        //    PlayerSensor.OnPlayerEnter -= PlayerSensorOnPlayerEnter;
        //    PlayerSensor.OnPlayerExit -= PlayerSensorOnPlayerExit;
        //}

        //private void PlayerSensorOnPlayerEnter(Transform Player)
        //{
        //    AgentBehaviour.SetGoal<AttackEnemy>(true);
        //}

        //private void PlayerSensorOnPlayerExit(Vector3 lastKnownPosition)
        //{
        //    AgentBehaviour.SetGoal<WanderGoal>(true);
        //}

    }

}
