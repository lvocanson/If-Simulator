using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collision2DProxy : MonoBehaviour
{
    [SerializeField] Collision2DEvent _event;

    private void Awake()
    {
        if (GetComponent<Collider2D>().isTrigger)
            Debug.LogWarning("Collision2DProxy: Collider is trigger. This will not raise collision events.", this);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        _event.Raise(other, CollisionType.Enter);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        _event.Raise(other, CollisionType.Exit);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        _event.Raise(other, CollisionType.Stay);
    }
}
