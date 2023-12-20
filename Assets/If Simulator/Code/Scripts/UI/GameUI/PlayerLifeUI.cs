using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _healthUpdateSpeed = 1f;

    
    public void UpdateHealth(float value, float maxHealth)
    {
        _healthSlider.value = value;
        _healthSlider.maxValue = maxHealth;
    }
}
