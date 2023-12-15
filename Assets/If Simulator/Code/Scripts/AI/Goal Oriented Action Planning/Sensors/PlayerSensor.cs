using UnityEngine;

namespace IfSimulator.GOAP.Sensors
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerSensor : MonoBehaviour
    {
        public CircleCollider2D Collider;

        public delegate void PlayerEnterEvent(Transform player);
        public delegate void PlayerExitEvent(Vector3 lastKnowPosition);

        public event PlayerEnterEvent OnPlayerEnter;
        public event PlayerExitEvent OnPlayerExit;

        private void Awake()
        {
            Collider = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
                OnPlayerEnter?.Invoke(player.transform);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
                OnPlayerExit?.Invoke(player.transform.position);
        }
    }
}
