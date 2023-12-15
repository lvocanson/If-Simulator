using System;
using NaughtyAttributes;
using UnityEngine;

public class DamageabaleEntity : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth;
    [ShowNonSerializedField] private float _currentHealth;
    
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    
    public event Action OnDeath;

    
    private void Start()
    {
        _currentHealth = _maxHealth;
    }
    
    public void Damage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
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
