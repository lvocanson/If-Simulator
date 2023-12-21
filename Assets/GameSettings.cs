using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Color _playerDamageColor;
    [SerializeField] private Color _enemyDamageColor;
    [SerializeField] private Color _healColor;
    //[SerializeField] private Color _invulnerabilityColor;
    //[SerializeField] private Color _deathColor;

    
    public Color PlayerDamageColor => _playerDamageColor;
    public Color EnemyDamageColor => _enemyDamageColor;
    public Color HealColor => _healColor;
    //public Color InvulnerabilityColor => _invulnerabilityColor;
    //public Color DeathColor => _deathColor;
    
}
