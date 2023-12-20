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
        [SerializeField] private PlayerSensor PlayerSensor;
        [SerializeField] private EnemySensor EnemySensor;
        [SerializeField] private HealConfigSO HealConfig;
        [SerializeField] private AttackConfigSO AttackConfig;
        [SerializeField] private CurrentPlayerSo CurrentPlayerSo;

        private AgentBehaviour AgentBehaviour;


        private void Awake()
        {
            AgentBehaviour = GetComponent<AgentBehaviour>();
        }

        private void Start()
        {
            AgentBehaviour.SetGoal<WanderGoal>(false);
            PlayerSensor.Collider.radius = HealConfig.SensorHealRadius;
            EnemySensor.Collider.radius = AttackConfig.SensorRadius;
        }

        private void OnEnable()
        {
            PlayerSensor.OnPlayerEnter += PlayerSensorOnPlayerEnter;
            PlayerSensor.OnPlayerStay += PlayerSensorOnPlayerStay;
            PlayerSensor.OnPlayerExit += PlayerSensorOnPlayerExit;
        }

        private void OnDisable()
        {
            PlayerSensor.OnPlayerEnter -= PlayerSensorOnPlayerEnter;
            PlayerSensor.OnPlayerStay -= PlayerSensorOnPlayerStay;
            PlayerSensor.OnPlayerExit -= PlayerSensorOnPlayerExit;
        }

        private void PlayerSensorOnPlayerEnter(Transform Player)
        {
            if (CurrentPlayerSo.Player.CurrentHealth < 90 )
            {
                AgentBehaviour.SetGoal<HealAlly>(true);
            }
            else if (CurrentPlayerSo.Player.CurrentHealth > 80)
            {
                AgentBehaviour.SetGoal<WanderGoal>(true);
            }
            else
            {
                AgentBehaviour.SetGoal<WanderGoal>(true);
            }
        }

        private void PlayerSensorOnPlayerStay(Transform Player)
        {
            if (CurrentPlayerSo.Player.CurrentHealth < 90)
            {
                AgentBehaviour.SetGoal<HealAlly>(true);
            }
            else if (CurrentPlayerSo.Player.CurrentHealth > 80)
            {
                AgentBehaviour.SetGoal<WanderGoal>(true);
            }
            else
            {
                AgentBehaviour.SetGoal<WanderGoal>(true);
            }
        }

        private void PlayerSensorOnPlayerExit(Vector3 lastKnownPosition)
        {
            AgentBehaviour.SetGoal<WanderGoal>(true);
        }
    }
}
