using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private PlayerLifeUI _playerLife;
    [SerializeField] private SpellHolderUI spellHolderUI;
    [SerializeField] private PassiveHolderUI passiveHolderUI;
    [SerializeField] private CurrentPlayerSo _currentPlayerSo;
    
    
    public SpellHolderUI SpellHolderUI => spellHolderUI;
    public PassiveHolderUI PassiveHolderUI => passiveHolderUI;


    private void OnEnable()
    {
        _currentPlayerSo.OnPlayerLoaded += LoadEvents;
    }
    
    private void LoadEvents()
    {
        if (_currentPlayerSo.Player == null)
        {
            Debug.LogError("Player is null");
            return;
        }
        _currentPlayerSo.Player.OnHealthChanged += _playerLife.UpdateHealth;
    }
    
    private void OnDisable()
    {
        _currentPlayerSo.OnPlayerLoaded -= LoadEvents;
        UnloadEvents();
    }
    
    private void UnloadEvents()
    {
        if (_currentPlayerSo.Player)
            _currentPlayerSo.Player.OnHealthChanged -= _playerLife.UpdateHealth;
    }

    private void Start()
    {
        SpellHolderUI.UpdateFirstSpell(null);
        SpellHolderUI.UpdateSecondSpell(null);
    }
}
