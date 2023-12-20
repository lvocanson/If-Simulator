using UnityEngine;
using UnityEngine.UI;

public class AbilityIconUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    
    public Image Icon => _icon;
    
    public void ChangeIcon(Sprite icon)
    {
        _icon.sprite = icon;
    }
    
    public void UpdateCooldown(float cooldown, float maxCooldown)
    {
        _icon.fillAmount = cooldown / maxCooldown;
    }
}
