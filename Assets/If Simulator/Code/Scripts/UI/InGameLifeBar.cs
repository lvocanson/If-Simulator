using UnityEngine;
using UnityEngine.UI;

public class InGameLifeBar : MonoBehaviour
{
    [SerializeField] private Slider _lifeSlider;
    
    private DamageableEntity _entity;
    private float _heightOffset;
    
    
    public void Initialize(Transform spawnPoint, DamageableEntity entity)
    {
        _entity = entity;
        _heightOffset = spawnPoint.localPosition.y;
    }
    
    private void Update()
    {
        transform.position = _entity.transform.position + Vector3.up * _heightOffset;
    }
    
    public void SetHealth(float currentHealth, float maxHealth)
    {
        _lifeSlider.maxValue = maxHealth;
        _lifeSlider.value = currentHealth;
    }
}
