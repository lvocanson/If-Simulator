using System;
using UnityEngine;

namespace Ability
{
    public class SecondaryFireExplosionBehavior : AbilityExplosionBehavior
    {
        [SerializeField] private LayerMask _wallLayer;
        
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
            if (other.TryGetComponent(out Rigidbody2D rb) is false || other.TryGetComponent(out IDamageable damageable) is false) return;
                
            // Assert that the other entity is not behind a wall
            if (Physics2D.Linecast(transform.position, other.transform.position, _wallLayer.value)) return;
                
            // Damage
            damageable.Damage(_so.Damage);
            
            // Push back
            Vector2 dir = (other.transform.position - transform.position).normalized;
            rb.AddForce(dir * _enemyPushBackForce, ForceMode2D.Impulse);
        }
    }
}