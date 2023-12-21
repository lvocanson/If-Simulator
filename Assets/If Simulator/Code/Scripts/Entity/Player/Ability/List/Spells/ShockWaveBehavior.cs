using UnityEngine;

namespace Ability
{
    public class ShockWaveBehavior : AbilityExplosionBehavior
    {
        protected override void HandleCollision(Collider2D other)
        {
            int otherLayer = other.gameObject.layer;
            // Get the binary value of the layer
            int otherLayerMask = 1 << otherLayer;
            
            // Skip unwanted layers
            bool isBullet = (otherLayerMask & _layersToCollide.value) == 0;
            bool isDamageableEntity = (otherLayerMask & _layersToDamage.value) == 0;
            if (isBullet is false && isDamageableEntity is false) return;
            
            // Destroy enemies' bullets
            if (otherLayerMask == _layersToCollide.value && other.CompareTag("PlayerProjectile") is false)
            {
                Destroy(other.gameObject);
                return;
            }
            
            // Push back enemies and damage them
            if (otherLayerMask != _layersToDamage.value) return;
            
            if (other.TryGetComponent(out Rigidbody2D rb) is false || other.TryGetComponent(out DamageableEntity damageable) is false) return;
                
            // Push back
            Vector2 dir = (other.transform.position - transform.position).normalized;
            rb.AddForce(dir * _enemyPushBackForce, ForceMode2D.Impulse);
                
            // Damage
            damageable.OnDeath += NotifyEnemyKilled;
            damageable.Damage(_so.Value);
            damageable.OnDeath -= NotifyEnemyKilled;
        }

        public override void OnUpdate()
        {
            _timer += Time.fixedDeltaTime / _so.AbilityDuration;
            float power = _evolutionCurve.Evaluate(_timer);
            
            _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, 1 - power);
            transform.localScale = Vector3.one * (power * _maxSize);
        }
    }
}