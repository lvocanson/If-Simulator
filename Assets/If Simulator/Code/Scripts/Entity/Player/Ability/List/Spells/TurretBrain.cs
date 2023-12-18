using System.Collections.Generic;
using Behaviors;
using FiniteStateMachine;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Pool;

namespace Ability
{
    public class TurretBrain : StateMachine
    {
        [Header("States")]
        [SerializeField] private TurretAttack _attackState;
        [SerializeField] private TurretSeek _seekState;
        [SerializeField] private TurretDestroy _destroyState;
        
        [Header("References")]
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private Transform _bulletContainer;
        
        [Header("Settings")]
        [SerializeField] private LayerMask _damageableLayer;
        [SerializeField] private int _numberOfBulletsPerDefault;
        [SerializeField] private int _numberOfBulletsMax;

        [Header("Debug")]
        [SerializeField, ReadOnly] private readonly List<IDamageable> _closeEntities = new List<IDamageable>();
        [SerializeField, ReadOnly] private IDamageable _currentTarget;
        
        private ObjectPool<GameObject> _bulletPool;

        public SoAbilityCooldown So
        {
            get => _so;
            set
            {
                _so = value;
                Debug.Log("Setting turret range to " + _so.Range + " units");
                _collider.radius = _so.Range;
            }
        }

        private SoAbilityCooldown _so;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            bool isValid = damageable != null && (_closeEntities.Contains(damageable) == false);
            
            // TODO FIX THIS
            bool isDamageableEntity = ((1 << other.gameObject.layer) & _damageableLayer.value) == 0;
            
            // Ignore unwanted layers and entities already in the list
            if (isValid is false || isDamageableEntity is false) return;
            
            // Add the new entity to the list
            _closeEntities.Add(damageable);
            
            // If there is no target, set the current target to the new one and change state
            if (_currentTarget != null) return;
            
            _currentTarget = damageable;
            ChangeState(_attackState, other.gameObject.transform);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            bool isValid = damageable != null && _closeEntities.Contains(damageable) == true;
            
            // Check if the entity is in the list, we do not need to check the layer here
            if (isValid is false) return;

            // Remove the entity from the list
            _closeEntities.Remove(damageable);
            if (_currentTarget != damageable) return;
            
            if (_closeEntities.Count > 0)
            {
                // Set the current target to the first entity in the list and change state
                _currentTarget = _closeEntities[0];
                ChangeState(_attackState, other.gameObject.transform);
            }
            else
            {
                // If there is no more entity in the list, set the current target to null and go back to seek state
                _currentTarget = null;
                ChangeState(_seekState);
            }
        }
        
        public void OnDeath()
        {
            Debug.Log("Destroying turret");
            // Handle turret explosion
            _currentTarget = null;
            ChangeState(_destroyState);
            
            // Destroy the game object
            Destroy(gameObject);
        }
    }
}