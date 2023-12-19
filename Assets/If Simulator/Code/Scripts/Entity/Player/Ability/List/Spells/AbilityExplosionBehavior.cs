using System;
using NaughtyAttributes;
using UnityEngine;

namespace Ability
{
    public abstract class AbilityExplosionBehavior : MonoBehaviour
    {
        [Header("Settings")]
        protected float _timer;
        [SerializeField] protected AnimationCurve _evolutionCurve;
        [SerializeField] protected float _maxSize;
        [SerializeField] protected float _enemyPushBackForce;
        [SerializeField, Tooltip("Alpha value will not be used since it will be modified by animation curve")] protected Color _color;
        
        protected float _damage;
        
        [Header("Layer Management")]
        [SerializeField] protected LayerMask _layersToCollide; 
        [SerializeField] protected LayerMask _layersToDamage;
        
        [Header("References")]
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField, ReadOnly] protected SoAbilityCooldown _so;

        public virtual void Init(SoAbilityCooldown so)
        {
            _timer = 0;
            _so = so;
        }

        public abstract void OnUpdate();
        
        protected abstract void HandleCollision(Collider2D other);

        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleCollision(other);
        }
    }
}