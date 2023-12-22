using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class DamageableEntity : MonoBehaviour, IDamageable
{
    private const float HIT_EFFECT_DURATION = 0.1f;

    
    [Header("References")]
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private AudioSource _audioSource;

    [Header("Damageable Entity")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _invulnerabilityDuration;
    
    [Header("Entity effects")]
    [SerializeField] private Material _hitMaterial;
    [SerializeField] private Material _invulnerabilityMaterial;
    [SerializeField] private float _scaleEffectOffset = 0.2f;
    [SerializeField] private Transform _damagePopupPosition;
    [SerializeField] private Transform _healPopupRotation;
    
    [Header("Feedback")]
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private GameObject _damageParticle;
    [SerializeField] private GameObject _dieParticle;
    
    [Header("Debug")]
    [ShowNonSerializedField] private float _currentHealth;
    
    
    private bool _isInvulnerable;
    private bool _isInvulnerableInternal;

    
    private Material _baseMaterial;
    private Color _baseColor;
    private Vector2 _baseSpriteScale;
    private TotalDamagePopup _totalDamagePopup;
    
    
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public float HealthPercentage => (_currentHealth / _maxHealth) * 100;
    public bool IsInvulnerable => _isInvulnerable || _isInvulnerableInternal;

    public event Action OnDamage;
    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath; // => float: xp to give to the player on death

    
    private Coroutine _effectsCoroutine;
    
    
    protected virtual void Awake()
    {
        _baseMaterial = _sprite.material;
        _baseColor = _sprite.color;
        _baseSpriteScale = _sprite.transform.localScale;
        _currentHealth = _maxHealth;
        _isInvulnerable = false;
    }

    protected virtual void Start()
    {
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }
    
    public void SetInvulnerable(bool isInvulnerable) => _isInvulnerable = isInvulnerable;
    
    public void Damage(float damage, Color color)
    {
        if (_currentHealth <= 0) return;
        if (IsInvulnerable) return;
        
        _currentHealth -= damage;
        
        if (_totalDamagePopup != null)
        {
            _totalDamagePopup.UpdateDamage((int)damage);
        }
        else
        {
            _totalDamagePopup = TotalDamagePopup.Create(transform, _damagePopupPosition.localPosition, (int)damage, color).GetComponent<TotalDamagePopup>();
        }
        
        SingleDamagePopup.Create(transform.position + _damagePopupPosition.localPosition, (int)damage, color);
        
        OnDamageTaken();
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
        
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
        if(_damageParticle != null)
            Instantiate(_damageParticle, transform.position, Quaternion.identity);
        if (_audioSource != null)
            _audioSource.PlayOneShot(_damageSound); 
    }
    
    public void Heal(float heal, Color color)
    {
        if (_currentHealth >= _maxHealth) return;
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + heal);
        
        SingleDamagePopup.Create(transform.position - _healPopupRotation.localPosition, (int)heal, LevelContext.Instance.GameSettings.HealColor);
        
        OnHealthChanged?.Invoke(_currentHealth, MaxHealth);
    }
    
    protected virtual void Die()
    {
        if (_dieParticle != null)
            Instantiate(_dieParticle, transform.position, Quaternion.identity);
        OnDeath?.Invoke();
        
        if (_totalDamagePopup != null)
        {
            Destroy(_totalDamagePopup.gameObject, 0.5f);
        }
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
