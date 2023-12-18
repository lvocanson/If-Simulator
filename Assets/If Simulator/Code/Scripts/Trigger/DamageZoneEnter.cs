using UnityEngine;

public class DamageZoneEnter : MonoBehaviour
{
    [SerializeField] private int _damage;
    
    private int _ignoreMask;
    
    
    public void IgnoreLayer(int layer)
    {
        _ignoreMask |= (1 << layer);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_ignoreMask & (1 << collision.gameObject.layer)) > 0) return;

        if (collision.TryGetComponent(out DamageabaleEntity damageabaleEntity)) damageabaleEntity.Damage(_damage);
    }
}
