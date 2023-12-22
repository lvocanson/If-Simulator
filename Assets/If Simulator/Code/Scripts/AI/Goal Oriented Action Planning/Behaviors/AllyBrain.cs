using System.Collections.Generic;
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
        
        private List<Enemy> _enemies = new();
        
        
        public void SetPlayer(Player player)
        {
            _player = player;
        }
        
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
            
            if (CurrentPlayerSo.Player.HealthPercentage <= HealConfig.HealThreshold)
            {
                AgentBehaviour.SetGoal<HealAlly>(true);
            }
            else if (_enemies.Count > 0)
            {
                AgentBehaviour.SetGoal<KillEnemy>(true);
            }
            else
            {
                AgentBehaviour.SetGoal<WanderGoal>(true);
            }
        }
        
        public void TeleportToPlayer()
        {
            if (_player)
            {
                transform.position = _player.transform.position;
            }
        }

        private void OnEnable()
        {
            EnemySensor.OnEnemyEnter += EnemySensorOnEnemyEnter;
            EnemySensor.OnEnemyExit += EnemySensorOnEnemyExit;
        }

        private void OnDisable()
        {
            EnemySensor.OnEnemyEnter -= EnemySensorOnEnemyEnter;
            EnemySensor.OnEnemyExit -= EnemySensorOnEnemyExit;
        }

        private void EnemySensorOnEnemyEnter(Transform Enemy)
        {
            _enemies.Add(Enemy.GetComponent<Enemy>());
        }
        
        private void EnemySensorOnEnemyExit(Transform enemy)
        {
            _enemies.Remove(enemy.GetComponent<Enemy>());
        }
    }
}
