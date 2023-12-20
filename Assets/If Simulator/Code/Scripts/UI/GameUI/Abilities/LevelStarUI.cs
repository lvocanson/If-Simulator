using UnityEngine;
using UnityEngine.UI;

public class LevelStarUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _icon;
    
    [Header("Settings")]
    [SerializeField] private Color _emptyStarColor;
    [SerializeField] private Color _filledStarColor;
    
    
    public void EnableStar(bool enable)
    {
        _icon.color = enable ? _filledStarColor : _emptyStarColor;
    }
}
