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
        [SerializeField] private HealConfigSO HealConfig;
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
        }

        private void OnEnable()
        {
            PlayerSensor.OnPlayerEnter += PlayerSensorOnPlayerEnter;
            PlayerSensor.OnPlayerExit += PlayerSensorOnPlayerExit;
        }

        private void OnDisable()
        {
            PlayerSensor.OnPlayerEnter -= PlayerSensorOnPlayerEnter;
            PlayerSensor.OnPlayerExit -= PlayerSensorOnPlayerExit;
        }

        private void PlayerSensorOnPlayerEnter(Transform Player)
        {
            Debug.Log(CurrentPlayerSo.Player.CurrentHealth + " " + "before");

            if (CurrentPlayerSo.Player.CurrentHealth < 60)
            {
                AgentBehaviour.SetGoal<HealAlly>(true);
                Debug.Log("Healing ally");
            }
            else
            {
                Debug.Log("Wander goal");
                AgentBehaviour.SetGoal<WanderGoal>(true);

            }

        }

        private void PlayerSensorOnPlayerExit(Vector3 lastKnownPosition)
        {
            AgentBehaviour.SetGoal<WanderGoal>(true);
        }
    }
}
