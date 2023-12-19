using System;
using System.Collections;
using UnityEngine;

namespace Ability
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private bool _managedFromPool;
        
        [Header("References")]
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _renderer;
        
        [Header("Data")]
        [SerializeField] private LayerMask _layers;
        [SerializeField] private LayerMask _damageableEntityLayers;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField, Min(0)] private float _damage;
        
        private Coroutine _selfDestructCoroutine;
        protected int _ownerLayer;
        private bool _isDestroyed;
        
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
        
        public event Action<Projectile> OnDestroy;
        public event Action OnHit;

        
        private void OnEnable()
        {
            _selfDestructCoroutine ??= StartCoroutine(SelfDestruct());
            _renderer.color = Color.white;
            _isDestroyed = false;
        }

        private void OnDisable()
        {
            if (_selfDestructCoroutine != null)
            {
                StopCoroutine(_selfDestructCoroutine);
                _selfDestructCoroutine = null;
            }
        }

        public void Initialize(int ownerId, Vector2 dir, bool managedByPool = false)
        {
            _ownerLayer = ownerId;
            _rb.velocity = dir * _speed;
            _managedFromPool = managedByPool;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            // skip unwanted layers
            int otherLayer = col.gameObject.layer;
            if (((1 << otherLayer) & _layers.value) == 0) return;
            if (otherLayer == _ownerLayer) return;


            if (_selfDestructCoroutine != null)
            {
                StopCoroutine(_selfDestructCoroutine);
                _selfDestructCoroutine = null;
            }
            
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
            float timer = 0;
            while (timer < _lifeTime)
            {
                timer += Time.deltaTime;

                if (_renderer)
                {
                    // fade
                    if (timer > _lifeTime - 0.2f)
                    {
                        Color color = _renderer.color;

                        float ratio = (timer - (_lifeTime - 0.2f)) / 0.2f;
                        color.a = Mathf.Lerp(1, 0, ratio);
                        _renderer.color = color;
                    }
                }

                yield return null;
            }

            Death();
        }
        
        private void Death()
        {
            _isDestroyed = true;
            OnDestroy?.Invoke(this);
            
            if (!_managedFromPool)
                Destroy(gameObject);
        }
    }
}