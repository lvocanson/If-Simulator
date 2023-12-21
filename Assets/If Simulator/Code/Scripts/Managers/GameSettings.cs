using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Color _playerDamageColor;
    [SerializeField] private Color _enemyDamageColor;
    [SerializeField] private Color _healColor;

    
    public Color PlayerDamageColor => _playerDamageColor;
    public Color EnemyDamageColor => _enemyDamageColor;
    public Color HealColor => _healColor;
    
}
