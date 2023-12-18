using UnityEngine;

namespace Ability
{
    public class ShockWaveBehavior : MonoBehaviour
    {
        [Header("Settings")]
        private float _timer;
        [SerializeField] private AnimationCurve _evolutionCurve;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _enemyPushBackForce;
        
        private float _damage;
        
        [Header("Layer Management")]
        [SerializeField] private LayerMask _projLayer; 
        [SerializeField] private LayerMask _damageableEntityLayers; 
        
        [Header("References")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            int otherLayer = other.gameObject.layer;
            
            // Skip unwanted layers
            bool isBullet = ((1 << otherLayer) & _projLayer.value) == 0;
            bool isDamageableEntity = ((1 << otherLayer) & _damageableEntityLayers.value) == 0;
            if (isBullet is false && isDamageableEntity is false) return;
            
            // Get the binary value of the layer
            int otherLayerMask = 1 << otherLayer;
            
            // Destroy enemies' bullets
            if (otherLayerMask == _projLayer.value && other.CompareTag("PlayerProjectile") is false)
                Destroy(other.gameObject);
            
            // Push back enemies and damage them
            else if (otherLayerMask == _damageableEntityLayers.value)
            {
                if (other.TryGetComponent(out Rigidbody2D rb) is false || other.TryGetComponent(out IDamageable damageable) is false) return;
                
                // Push back
                Vector2 dir = (other.transform.position - transform.position).normalized;
                rb.AddForce(dir * _enemyPushBackForce, ForceMode2D.Impulse);
                
                // Damage
                damageable.Damage(_damage);
            }
        }

        public void Init(float damage)
        {
            _timer = 0;
            _damage = damage;
        }

        public void OnUpdate(float duration)
        {
            _timer += Time.fixedDeltaTime / duration;
            float power = _evolutionCurve.Evaluate(_timer);
            
            _spriteRenderer.color = new Color(0.5f, 0.5f, 0.9f, 1 - power );
            transform.localScale = Vector3.one * (power * _maxSize);
            
            //_collider.radius = power * _maxSize;
        }
    }
}