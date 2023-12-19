using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private SpellHolder _spellHolder;
    [SerializeField] private PassiveHolder _passiveHolder;
    
    public Slider HealthSlider => _healthSlider;
    public SpellHolder SpellHolder => _spellHolder;
    public PassiveHolder PassiveHolder => _passiveHolder;
    
    
    private void Start()
    {
        UpdateMaxHealth(100);
        UpdatePlayerHealth(100);
    }
    
    private void UpdatePlayerHealth(float value)
    {
        _healthSlider.value = value;
    }
    
    private void UpdateMaxHealth(float value)
    {
        _healthSlider.maxValue = value;
    }
}
