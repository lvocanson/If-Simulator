using System;
using UnityEngine;

namespace Ability.List
{
    public class BulletBehavior : MonoBehaviour
    {
        public float Damage { get; set; }
        
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        private Rigidbody2D _rb;
        
        private void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _rb.velocity = transform.up * _speed;
        }

        private void Update()
        {
            _lifeTime -= Time.deltaTime;
            if (_lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}