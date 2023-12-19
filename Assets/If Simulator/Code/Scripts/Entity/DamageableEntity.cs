using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class DamageableEntity : MonoBehaviour, IDamageable
{
    private const float HIT_EFFECT_DURATION = 0.1f;

    
    [Header("References")]
    [SerializeField] private SpriteRenderer _sprite;

    [Header("Damageable Entity")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _invulnerabilityDuration;
    
    [Header("Entity effects")]
    [SerializeField] private Material _hitMaterial;
    [SerializeField] private Material _invulnerabilityMaterial;
    [SerializeField] private float _scaleEffectOffset = 0.2f;
    
    [Header("Debug")]
    [ShowNonSerializedField] private float _currentHealth;
    
    
    private bool _isInvulnerable;
    private bool _isInvulnerableInternal;

    
    private Material _baseMaterial;
    private Color _baseColor;
    private Vector2 _baseSpriteScale;
    
    
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public bool IsInvulnerable => _isInvulnerable || _isInvulnerableInternal;

    public event Action OnHit;
    public event Action OnDamage;
    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    
    private Coroutine _effectsCoroutine;
    
    
    protected virtual void Awake()
    {
        _baseMaterial = _sprite.material;
        _baseColor = _sprite.color;
        _baseSpriteScale = _sprite.transform.localScale;
        
        _isInvulnerable = false;
    }

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
    }
    
    public void SetInvulnerable(bool isInvulnerable) => _isInvulnerable = isInvulnerable;
    
    public void Damage(float damage)
    {
        if (_currentHealth <= 0) return;
        if (_isInvulnerable) return;
        
        _currentHealth -= damage;
        OnHit?.Invoke();
        
        StartCoroutine(InternalInvulnerability());
        if (_effectsCoroutine != null)
        {
            StopCoroutine(_effectsCoroutine);
        }
        _effectsCoroutine = StartCoroutine(StartEffects());

        if (_currentHealth <= 0) Die();
    }

    protected virtual void OnDamageTaken()
    {
        OnDamage?.Invoke();
    }
    
    public bool Heal(float heal)
    {
        if (_currentHealth >= _maxHealth) return false;
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + heal);
        
        OnHealthChanged?.Invoke(_currentHealth, MaxHealth);

        return true;
    }
    
    protected virtual void Die()
    {
        OnDeath?.Invoke();
    }
    
    public void Kill()
    {
        Die();
    }
    
    
    #region Hit Effect
    
    private IEnumerator StartEffects()
    {
        _sprite.color = Color.white;
        _sprite.material = _hitMaterial;
        
        yield return ScaleEffect(HIT_EFFECT_DURATION);

        if (_invulnerabilityDuration - HIT_EFFECT_DURATION > 0)
        {
            _sprite.color = _baseColor;
            _sprite.material = _invulnerabilityMaterial;

            yield return new WaitForSeconds(_invulnerabilityDuration);
        }
        EffectsEnd();

        _effectsCoroutine = null;
    }

    private IEnumerator InternalInvulnerability()
    {
        _isInvulnerableInternal = true;
        yield return new WaitForSeconds(_invulnerabilityDuration);
        _isInvulnerableInternal = false;
    }
    
    private IEnumerator ScaleEffect(float duration)
    {
        float timer = duration;
        float lerpVal = 0f;
        while (timer > 0)
        {
            if (timer > duration * 0.5f)
            {
                lerpVal += Time.deltaTime / duration;

                float xMult = Mathf.Lerp(1f, 1 - _scaleEffectOffset, lerpVal * 2f);
                float yMult = Mathf.Lerp(1f, 1 + _scaleEffectOffset, lerpVal * 2f);
                _sprite.transform.localScale = new Vector3(_baseSpriteScale.x * xMult, _baseSpriteScale.y * yMult, 1);
            }
            else
            {
                lerpVal += Time.deltaTime / duration;

                float xMult = Mathf.Lerp(1 - _scaleEffectOffset, 1f, (lerpVal - 0.5f) * 2f);
                float yMult = Mathf.Lerp(1 + _scaleEffectOffset, 1f, (lerpVal - 0.5f) * 2f);
                _sprite.transform.localScale = new Vector3(_baseSpriteScale.x * xMult, _baseSpriteScale.y * yMult, 1);
            }

            timer -= Time.deltaTime;
            
            yield return null;
        }

        _sprite.transform.localScale = _baseSpriteScale;
    }

    private void EffectsEnd()
    {
        _isInvulnerableInternal = false;

        if((_hitMaterial != null && _sprite.material.shader == _hitMaterial.shader) || (_invulnerabilityMaterial != null && _sprite.material.shader == _invulnerabilityMaterial.shader))
            _sprite.material = _baseMaterial;

        _sprite.color = _baseColor;

        _effectsCoroutine = null;
    }
    
    #endregion
    
    protected virtual void OnDisable()
    {
        if (_effectsCoroutine != null)
        {
            StopCoroutine(_effectsCoroutine);
            EffectsEnd();
        }
    }
}