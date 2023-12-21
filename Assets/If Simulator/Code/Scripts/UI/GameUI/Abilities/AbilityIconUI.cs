using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIconUI : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Color _highlightColor;
    
    [SerializeField] private TextMeshProUGUI _cooldownText;
    
    [SerializeField] private Image _icon;
    [SerializeField] private Color _baseColor;
    
    public Image Icon => _icon;
    
    public void ChangeIcon(Sprite icon)
    {
        _icon.sprite = icon;
    }
    
    public void OnUsed()
    {
        _background.color = _highlightColor;
    }
    
    public void UpdateCooldown(float cooldown, float maxCooldown)
    {
        _background.color = _baseColor;
        _icon.fillAmount = 1 - (cooldown / maxCooldown);
        
        _cooldownText.gameObject.SetActive(cooldown > 0);
        _cooldownText.text = ((int)cooldown).ToString();
    }
}
