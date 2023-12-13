using UnityEngine;

public class DamageabaleEntity : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    private float _health;

    private void Start()
    {
        _health = _maxHealth;
    }
    
    public void ApplyDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
