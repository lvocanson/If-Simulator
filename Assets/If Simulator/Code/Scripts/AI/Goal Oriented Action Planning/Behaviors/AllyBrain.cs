using System;
using CrashKonijn.Goap.Behaviours;
using IfSimulator.GOAP.Config;
using IfSimulator.GOAP.Goals;
using IfSimulator.GOAP.Sensors;
using UnityEngine;
using UnityEngine.AI;

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
        [SerializeField] private NavMeshAgent NavMeshAgent;
        
        private AgentBehaviour AgentBehaviour;
        private Player _player;
        
        private void Awake()
        {
            AgentBehaviour = GetComponent<AgentBehaviour>();
            NavMeshAgent.updateRotation = false;
            NavMeshAgent.updateUpAxis = false;
        }

        private void Start()
        {
            AgentBehaviour.SetGoal<WanderGoal>(false);
            PlayerSensor.Collider.radius = HealConfig.SensorHealRadius;
            EnemySensor.Collider.radius = AttackConfig.SensorRadius;
        }

        private void Update()
        {
            if (_player)
            {
                NavMeshAgent.SetDestination(_player.AllyTarget.position);
            }
        }

        private void OnEnable()
        {
            PlayerSensor.OnPlayerEnter += PlayerSensorOnPlayerEnter;
            PlayerSensor.OnPlayerStay += PlayerSensorOnPlayerStay;
            PlayerSensor.OnPlayerExit += PlayerSensorOnPlayerExit;

            EnemySensor.OnEnemyEnter += EnemySensorOnEnemyEnter;
            EnemySensor.OnEnemyStay += EnemySensorOnEnemyStay;
            EnemySensor.OnEnemyExit += EnemySensorOnEnemyExit;
            
            CurrentPlayerSo.OnPlayerLoaded += LoadPlayer;
        }

        private void OnDisable()
        {
            PlayerSensor.OnPlayerEnter -= PlayerSensorOnPlayerEnter;
            PlayerSensor.OnPlayerStay -= PlayerSensorOnPlayerStay;
            PlayerSensor.OnPlayerExit -= PlayerSensorOnPlayerExit;

            EnemySensor.OnEnemyEnter -= EnemySensorOnEnemyEnter;
            EnemySensor.OnEnemyStay -= EnemySensorOnEnemyStay;
            EnemySensor.OnEnemyExit -= EnemySensorOnEnemyExit;
            
            CurrentPlayerSo.OnPlayerLoaded -= LoadPlayer;
        }
        
        private void LoadPlayer()
        {
            _player = CurrentPlayerSo.Player;
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
        }

        private void PlayerSensorOnPlayerExit(Vector3 lastKnownPosition)
        {
            AgentBehaviour.SetGoal<WanderGoal>(true);
        }

        private void EnemySensorOnEnemyEnter(Transform Enemy)
        {
            AgentBehaviour.SetGoal<KillEnemy>(true);
        }

        private void EnemySensorOnEnemyStay(Transform Enemy)
        {
            AgentBehaviour.SetGoal<KillEnemy>(true);
        }

        private void EnemySensorOnEnemyExit(Vector3 lastKnownPosition)
        {
            AgentBehaviour.SetGoal<WanderGoal>(true);
        }
    }
}
