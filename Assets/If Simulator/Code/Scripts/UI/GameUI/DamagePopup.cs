using System;
using TMPro;
using UnityEngine;

public abstract class DamagePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected TextMeshPro _textMesh;
    
    [Header("Base Settings")]
    [SerializeField] protected float _duration = 1f;
    [SerializeField] private float _fadeSpeed = 1f;
    
    
    protected Color _textColor;
    protected float _disappearTimer;


    public virtual void Setup(int damageAmount)
    {
        _disappearTimer = _duration;
        _textMesh.SetText(damageAmount.ToString());
        _textColor = _textMesh.color;
    }

    protected virtual void Update()
    {
        _disappearTimer -= Time.deltaTime;
        
        if (_disappearTimer <= 0)
        {
            _textColor.a -= _fadeSpeed * Time.deltaTime;
            _textMesh.color = _textColor;
            
            if (_textColor.a <= 0)
                Destroy(gameObject);
        }
    }
}
