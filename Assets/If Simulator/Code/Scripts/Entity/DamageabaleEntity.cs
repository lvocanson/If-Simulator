using System;
using UnityEngine;

public class DamageabaleEntity : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    private float _health;
    
    public event Action OnDeath;

    
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
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
