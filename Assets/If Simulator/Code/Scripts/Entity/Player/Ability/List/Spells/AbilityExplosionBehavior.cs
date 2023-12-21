using System;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Ability
{
    public abstract class AbilityExplosionBehavior : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] protected AnimationCurve _evolutionCurve;
        [SerializeField] protected float _maxSize;
        [SerializeField] protected float _enemyPushBackForce;
        [SerializeField, Tooltip("Alpha value will not be used since it will be modified by animation curve")] protected Color _color;
        protected float _timer;
        
        [Header("Layer Management")]
        [SerializeField] protected LayerMask _layersToCollide; 
        [SerializeField] protected LayerMask _layersToDamage;
        
        [Header("References")]
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField, ReadOnly] protected SoAbilityCooldown _so;
        [SerializeField, ReadOnly, CanBeNull] protected AbilityActive _parentAbility;

        [Header("Events")] 
        [SerializeField] protected UnityEvent _onEnemyKilled;

        public virtual void Init(SoAbilityCooldown so, AbilityActive parentAbility)
        {
            _timer = 0;
            _so = so;
            _parentAbility = parentAbility;
        }

        public abstract void OnUpdate();
        
        /// <summary>
        /// Handles the collision with the collider of the explosion
        /// </summary>
        /// <param name="other"> The element the explosion's collider collided with </param>
        /// <returns>The number of entities killed by the explosion</returns>
        protected abstract void HandleCollision(Collider2D other);

        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleCollision(other);
        }
        
        protected void NotifyEnemyKilled()
        {
            if (_parentAbility != null)
                _parentAbility.TriggerEnemyKilled();
        }
    }
}