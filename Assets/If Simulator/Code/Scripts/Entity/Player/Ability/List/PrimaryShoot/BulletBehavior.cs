using System;
using System.Collections;
using UnityEngine;

namespace Ability
{
    public class BulletBehavior : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D _rb;
        
        [Header("Data")]
        [SerializeField] private LayerMask _layers;
        [SerializeField] private LayerMask _damageableEntityLayers;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField, Min(0)] private float _damage;
        
        protected int _ownerLayer;

        public float Damage => _damage;
        
        public void SetDamage(float damage)
        {
            if (damage >= 0)
                _damage = damage;
            else 
                Debug.LogWarning("Damage must be greater than 0");
        }
        public void SetSpeed(float speed)
        {
            if (speed > 0)
                _speed = speed;
            else 
                Debug.LogWarning("Speed must be greater than 0");
        }
        public void SetLifeTime(float lifetime)
        {
            if (lifetime > 0)
                _lifeTime = lifetime;
            else 
                Debug.LogWarning("Lifetime must be greater than 0");
        }
        
        public event Action OnDestroy;
        public event Action OnHit;
        
        private void Start()
        {
            StartCoroutine(SelfDestruct());
        }
        
        public void Initialize(int ownerId, Vector2 dir)
        {
            _ownerLayer = ownerId;
            _rb.velocity = dir * _speed;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            // skip unwanted layers
            int otherLayer = col.gameObject.layer;
            if (((1 << otherLayer) & _layers.value) == 0) return;
            if (otherLayer == _ownerLayer) return;
            
            StopCoroutine(SelfDestruct());

            // damage
            if (((1 << otherLayer) & _damageableEntityLayers.value) != 0)
            {
                if (col.TryGetComponent(out IDamageable damageable))
                {
                    if (_damage > 0)
                    {
                        damageable.Damage(_damage);
                    }
                    OnHit?.Invoke();
                }
                else
                {
                    return;
                }
            }

            Death();
        }
        
        private IEnumerator SelfDestruct()
        {
            SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();

            float timer = 0;
            while (timer < _lifeTime)
            {
                timer += Time.deltaTime;

                if (renderer)
                {
                    // fade
                    if (timer > _lifeTime - 0.2f)
                    {
                        Color color = renderer.color;

                        float ratio = (timer - (_lifeTime - 0.2f)) / 0.2f;
                        color.a = Mathf.Lerp(1, 0, ratio);
                        renderer.color = color;
                    }
                }

                yield return null;
            }

            Destroy(gameObject);
        }
        
        private void Death()
        {
            Destroy(gameObject);
        }
    }
}