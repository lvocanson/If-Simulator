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
        [SerializeField] private Color _color;
        
        [Header("Data")]
        [SerializeField] private LayerMask _layers;
        [SerializeField] private LayerMask _damageableEntityLayers;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField, Min(0)] private float _damage;
        
        private Coroutine _selfDestructCoroutine;
        protected int _ownerLayer;
        private Color _damageColor;
        
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
        public event Action OnEntityKill;

        
        private void OnEnable()
        {
            _selfDestructCoroutine ??= StartCoroutine(SelfDestruct());
            _renderer.color = _color;
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
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            
            _ownerLayer = ownerId;
            _rb.velocity = dir.normalized * _speed;
            _managedFromPool = managedByPool;
            
            if (_ownerLayer == LayerMask.NameToLayer("Player") || _ownerLayer == LayerMask.NameToLayer("Ally"))
                _damageColor = LevelContext.Instance.GameSettings.PlayerDamageColor;
            else
                _damageColor = LevelContext.Instance.GameSettings.EnemyDamageColor;
        }
        
        private void OnEntityDeath()
        {
            OnEntityKill?.Invoke();
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
                if (col.TryGetComponent(out DamageableEntity damageable))
                {
                    if (_damage > 0)
                    {
                        damageable.OnDeath += OnEntityDeath;
                        damageable.Damage(_damage, _damageColor);
                        damageable.OnDeath -= OnEntityDeath;
                    }
                    OnHit?.Invoke();
                }
                else if (col.TryGetComponent(out IDamageable Idamageable))
                {
                    if (_damage > 0)
                    {
                        Idamageable.Damage(_damage, _damageColor);
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
            OnDestroy?.Invoke(this);
            
            if (!_managedFromPool)
                Destroy(gameObject);
        }
    }
}