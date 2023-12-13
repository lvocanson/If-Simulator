using UnityEngine;

namespace Ability.List
{
    public class BulletBehavior : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _rb;
        
        private void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _rb.velocity = transform.up * _speed;
        }
    }
}