using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private PlayerLifeUI _playerLife;
    [SerializeField] private SpellHolderUI _spellHolderUI;
    [SerializeField] private PassiveHolderUI _passiveHolderUI;
    [SerializeField] private CurrentPlayerSo _currentPlayerSo;
    
    
    public SpellHolderUI SpellHolderUI => _spellHolderUI;
    public PassiveHolderUI PassiveHolderUI => _passiveHolderUI;


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
        _currentPlayerSo.Player.PlayerAttackManager.OnFirstSpellChanged += _spellHolderUI.UpdateFirstSpell;
        _currentPlayerSo.Player.PlayerAttackManager.OnSecondSpellChanged += _spellHolderUI.UpdateSecondSpell;
    }
    
    private void OnDisable()
    {
        _currentPlayerSo.OnPlayerLoaded -= LoadEvents;
        UnloadEvents();
    }
    
    private void UnloadEvents()
    {
        if (_currentPlayerSo.Player == null)
        {
            Debug.LogError("Player is null");
            return;
        }
        
        _currentPlayerSo.Player.OnHealthChanged -= _playerLife.UpdateHealth;
        _currentPlayerSo.Player.PlayerAttackManager.OnFirstSpellChanged -= _spellHolderUI.UpdateFirstSpell;
        _currentPlayerSo.Player.PlayerAttackManager.OnSecondSpellChanged -= _spellHolderUI.UpdateSecondSpell;
    }

    private void Start()
    {
    }
}
