﻿using System;
using Unity.Mathematics;
using UnityEngine;

namespace Ability
{
    public class SecondaryFireExplosionBehavior : AbilityExplosionBehavior
    {
        [SerializeField] private LayerMask _wallLayer;
        
        [Header("Feedback")]
        [SerializeField] private GameObject _particles;

        private void Start()
        {
            Instantiate(_particles, transform.position, quaternion.identity);
        }

        private void Update()
        {
            OnUpdate();
        }

        public override void OnUpdate()
        {
            _timer += Time.fixedDeltaTime / _so.AbilityDuration;
            float power = _evolutionCurve.Evaluate(_timer);

            _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, 1 - power);
            transform.localScale = Vector3.one * (power * _maxSize);
            
            if (_timer >= 1)
            {
                Destroy(gameObject);
            }
        }

        protected override void HandleCollision(Collider2D other)
        {
            int otherLayer = other.gameObject.layer;
            
            // Skip unwanted layers
            bool isBullet = ((1 << otherLayer) & _layersToCollide.value) == 0;
            bool isDamageableEntity = ((1 << otherLayer) & _layersToDamage.value) == 0;
            if (isBullet is false && isDamageableEntity is false) return;
            
            // Get the binary value of the layer
            int otherLayerMask = 1 << otherLayer;
            
            // Damage enemies not behind a wall
            if (otherLayerMask != _layersToDamage.value) return;
            
            // Assert that the other entity is damageable
            if (other.TryGetComponent(out DamageableEntity damageable) is false) return;
                
            // Assert that the other entity is not behind a wall
            if (Physics2D.Linecast(transform.position, other.transform.position, _wallLayer.value)) return;
                
            // Damage
            damageable.OnDeath += NotifyEnemyKilled;
            damageable.DamageWithoutInvulnerability(_so.Value, LevelContext.Instance.GameSettings.PlayerDamageColor);
            damageable.OnDeath -= NotifyEnemyKilled;
        }
    }
}