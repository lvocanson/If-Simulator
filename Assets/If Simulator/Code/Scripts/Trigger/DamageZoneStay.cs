using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageZoneStay : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private bool _continuousDamage;
    
    public event Action OnHit;
    public event Action OnEnemyHit;

    private string _ownerTag;
    private bool _isAlreadyTriggered;

    private List<DamageabaleEntity> _hitEnemies = new();

    
    private void Awake()
    {
        _ownerTag = gameObject.tag;
    }
    
    public void SetTag(string tag)
    {
        _ownerTag = tag;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        _hitEnemies.Clear();
    }
    
    public void Deactivate()
    {
        gameObject.SetActive(false);
        _hitEnemies.Clear();
        _isAlreadyTriggered = false;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(_ownerTag)) return;
        if (!_isAlreadyTriggered)
        {
            _isAlreadyTriggered = true;
            OnHit?.Invoke();
        }
        
        if (!collision.TryGetComponent(out DamageabaleEntity entity)) return;
        if (!entity.enabled) return;
        if (_hitEnemies.Contains(entity)) return;

        if (!_continuousDamage)
            _hitEnemies.Add(entity);
        
        OnEnemyHit?.Invoke();
        entity.Damage(_damage);
    }
}
