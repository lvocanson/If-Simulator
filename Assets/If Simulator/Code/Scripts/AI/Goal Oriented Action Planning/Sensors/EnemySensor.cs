using System;
using UnityEngine;

namespace IfSimulator.GOAP.Sensors
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class EnemySensor : MonoBehaviour
    {
        public CircleCollider2D Collider;

        public event Action<Transform> OnEnemyEnter;
        public event Action<Transform> OnEnemyStay;
        public event Action<Transform> OnEnemyExit;

        private void Awake()
        {
            Collider = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var p = collision.GetComponent<Enemy>();

            if (p != null)
            {
                OnEnemyEnter?.Invoke(p.transform);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            var p = collision.GetComponent<Enemy>();

            if (p != null)
            {
                OnEnemyStay?.Invoke(p.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var p = collision.GetComponent<Enemy>();

            if (p != null)
            {
                OnEnemyExit?.Invoke(p.transform);
            }
        }
    }
}
