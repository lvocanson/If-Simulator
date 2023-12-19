using Ability;
using FiniteStateMachine;
using UnityEngine;

namespace Behaviors
{
    public class TurretDestroy : BaseState
    {
        [SerializeField] private GameObject _explosionPrefab;
        [SerializeField] private Transform _explosionSpawnPoint;

        [SerializeField] private SoAbilityCooldown _so;
        
        private AbilityExplosionBehavior _explosionInstance;
        
        public override void Enter(StateMachine manager, params object[] args)
        {
            base.Enter(manager, args);
            GameObject go = Instantiate(_explosionPrefab, _explosionSpawnPoint.position, Quaternion.identity);
            _explosionInstance = go.GetComponent<AbilityExplosionBehavior>();
            _explosionInstance.Init(_so);
        }
    }
}