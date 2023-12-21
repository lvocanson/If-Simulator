using If_Simulator.Code.Scripts.UI.GameUI;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private PlayerLifeUI _playerLife;
    [SerializeField] private PlayerXpUI _playerXp;
    [SerializeField] private SpellHolderUI _spellHolderUI;
    [SerializeField] private PassiveHolderUI _passiveHolderUI;
    [SerializeField] private CurrentPlayerSo _currentPlayerSo;
    
    public SpellHolderUI SpellHolderUI => _spellHolderUI;
    public PassiveHolderUI PassiveHolderUI => _passiveHolderUI;


    private void OnEnable()
    {
        _currentPlayerSo.OnPlayerLoaded += LoadEvents;
        _currentPlayerSo.OnPlayerStarted += LoadSpellsEvents;
    }
    
    private void LoadEvents()
    {
        if (_currentPlayerSo.Player == null)
        {
            Debug.LogError("Player is null");
            return;
        }
        _currentPlayerSo.Player.OnHealthChanged += _playerLife.UpdateHealth;
        _currentPlayerSo.Player.PlayerXp.OnXpChanged += _playerXp.UpdateValue; 

        _currentPlayerSo.Player.PlayerAttackManager.OnFirstSpellChanged += _spellHolderUI.UpdateFirstSpell;
        _currentPlayerSo.Player.PlayerAttackManager.OnSecondSpellChanged += _spellHolderUI.UpdateSecondSpell;
    }

    private void LoadSpellsEvents()
    {
        _currentPlayerSo.Player.PlayerAttackManager.FirstSpell.OnCooldownChanged += _spellHolderUI.UpdateFirstSpellCooldown;
        _currentPlayerSo.Player.PlayerAttackManager.SecondSpell.OnCooldownChanged += _spellHolderUI.UpdateSecondSpellCooldown;
        
        _currentPlayerSo.Player.PlayerAttackManager.FirstSpell.OnAbilityActivated += _spellHolderUI.OnFirstSpellActivated;
        _currentPlayerSo.Player.PlayerAttackManager.SecondSpell.OnAbilityActivated += _spellHolderUI.OnSecondSpellActivated;
    }
    
    private void OnDisable()
    {
        _currentPlayerSo.OnPlayerLoaded -= LoadEvents;
        _currentPlayerSo.OnPlayerStarted -= LoadSpellsEvents;
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
        _currentPlayerSo.Player.PlayerXp.OnXpChanged -= _playerXp.UpdateValue;
        
        _currentPlayerSo.Player.PlayerAttackManager.OnFirstSpellChanged -= _spellHolderUI.UpdateFirstSpell;
        _currentPlayerSo.Player.PlayerAttackManager.OnSecondSpellChanged -= _spellHolderUI.UpdateSecondSpell;
        
        _currentPlayerSo.Player.PlayerAttackManager.FirstSpell.OnCooldownChanged -= _spellHolderUI.UpdateFirstSpellCooldown;
        _currentPlayerSo.Player.PlayerAttackManager.SecondSpell.OnCooldownChanged -= _spellHolderUI.UpdateSecondSpellCooldown;
        
        _currentPlayerSo.Player.PlayerAttackManager.FirstSpell.OnAbilityActivated -= _spellHolderUI.OnFirstSpellActivated;
        _currentPlayerSo.Player.PlayerAttackManager.SecondSpell.OnAbilityActivated -= _spellHolderUI.OnSecondSpellActivated;
    }

    private void Start()
    {
    }
}
