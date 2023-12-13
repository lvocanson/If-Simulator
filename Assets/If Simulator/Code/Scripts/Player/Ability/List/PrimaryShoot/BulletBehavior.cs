using System;
using System.Collections;
using UnityEngine;

namespace Ability
{
    public class BulletBehavior : MonoBehaviour
    {
        public float Damage { get; set; }
        
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private Rigidbody2D _rb;

        public event Action OnDestroy;
        
        private void OnEnable()
        {
            StartCoroutine(DestroyAfterDelay());
            _rb.velocity = transform.up * _speed;
        }
        
        private IEnumerator DestroyAfterDelay()
        {
            float timer = 0f;
            while (timer < _lifeTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            
            OnDestroy?.Invoke();
        }
    }
}