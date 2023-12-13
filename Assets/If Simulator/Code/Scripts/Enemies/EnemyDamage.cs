using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private int _ignoreMask;
    
    
    public void IgnoreLayer(int layer)
    {
        _ignoreMask |= (1 << layer);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_ignoreMask & (1 << collision.gameObject.layer)) > 0) return;

        if (collision.TryGetComponent<DamageabaleEntity>(out DamageabaleEntity damageabaleEntity))
        {
            damageabaleEntity.ApplyDamage(10f);
        }
    }
}
