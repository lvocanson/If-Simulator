using System;
using UnityEngine;

namespace IfSimulator.GOAP.Sensors
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerSensor : MonoBehaviour
    {
        public CircleCollider2D Collider;

        public event Action<Transform> OnPlayerEnter;
        public event Action<Vector3> OnPlayerExit;

        private void Awake()
        {
            Collider = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var p = collision.GetComponentInChildren<Player>();
            if (p != null)
            {
                OnPlayerEnter?.Invoke(p.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var p = collision.GetComponentInChildren<Player>();

            if (p != null)
            {
                OnPlayerExit?.Invoke(p.transform.position);
            }
        }
    }
}
