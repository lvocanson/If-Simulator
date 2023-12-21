using System;
using Ability;
using If_Simulator.Code.Scripts.UI.GameUI;
using UI;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private PlayerLifeUI _playerLife;
    [SerializeField] private PlayerXpUI _playerXp;
    [SerializeField] private SpellHolderUI _spellHolderUI;
    [SerializeField] private PassiveHolderUI _passiveHolderUI;
    [SerializeField] private CurrentPlayerSo _currentPlayerSo;
    
    [SerializeField] private SpellChoicePopup _spellChoicePopup;
    
    public SpellHolderUI SpellHolderUI => _spellHolderUI;
    public PassiveHolderUI PassiveHolderUI => _passiveHolderUI;
    public CurrentPlayerSo CurrentPlayerSo => _currentPlayerSo;


    private void Start()
    {
        _spellHolderUI.FirstSpell.HideAbility();
        _spellHolderUI.SecondSpell.HideAbility();
    }

    private void OnEnable()
    {
        _currentPlayerSo.OnPlayerLoaded += LoadEvents;
    }
    
    public void ChangeFirstSpell(SoAbilityBase so)
    {
        Debug.Log("Changed first spell");

        AbilityActive ability = _currentPlayerSo.Player.PlayerAttackManager.FirstSpell;

        if (ability)
        {
            ability.OnAbilityActivated -= _spellHolderUI.OnFirstSpellActivated;
            ability.OnCooldownChanged -= _spellHolderUI.UpdateFirstSpellCooldown;
        }
        else
        {
            _spellHolderUI.FirstSpell.ShowAbility();
        }
        
        _currentPlayerSo.Player.PlayerAttackManager.ChangeFirstSpell(so);
        
        ability = _currentPlayerSo.Player.PlayerAttackManager.FirstSpell;
        ability.OnCooldownChanged += _spellHolderUI.UpdateFirstSpellCooldown;
        ability.OnAbilityActivated += _spellHolderUI.OnFirstSpellActivated;
    }
    
    public void ChangeSecondSpell(SoAbilityBase so)
    {
        Debug.Log("Changed second spell");
        AbilityActive ability = _currentPlayerSo.Player.PlayerAttackManager.SecondSpell;

        if (ability)
        {
            ability.OnAbilityActivated -= _spellHolderUI.OnSecondSpellActivated;
            ability.OnCooldownChanged -= _spellHolderUI.UpdateSecondSpellCooldown;
        }
        else
        {
            _spellHolderUI.SecondSpell.ShowAbility();
        }

        _currentPlayerSo.Player.PlayerAttackManager.ChangeSecondSpell(so);
        
        ability = _currentPlayerSo.Player.PlayerAttackManager.SecondSpell;
        ability.OnCooldownChanged += _spellHolderUI.UpdateSecondSpellCooldown;
        ability.OnAbilityActivated += _spellHolderUI.OnSecondSpellActivated;
    }
    
    private void LoadEvents()
    {
        if (_currentPlayerSo.Player == null)
        {
            Debug.LogError("Player is null");
            return;
        }
        // Health change event
        _currentPlayerSo.Player.OnHealthChanged += _playerLife.UpdateHealth;
        
        // Xp change events
        _currentPlayerSo.Player.PlayerXp.OnXpChanged += _playerXp.UpdateValue; 
        _currentPlayerSo.Player.PlayerXp.OnLevelUp += _spellChoicePopup.Init;

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
        _currentPlayerSo.Player.PlayerXp.OnXpChanged -= _playerXp.UpdateValue;
        
        _currentPlayerSo.Player.PlayerAttackManager.OnFirstSpellChanged -= _spellHolderUI.UpdateFirstSpell;
        _currentPlayerSo.Player.PlayerAttackManager.OnSecondSpellChanged -= _spellHolderUI.UpdateSecondSpell;
        
        _currentPlayerSo.Player.PlayerAttackManager.FirstSpell.OnCooldownChanged -= _spellHolderUI.UpdateFirstSpellCooldown;
        _currentPlayerSo.Player.PlayerAttackManager.SecondSpell.OnCooldownChanged -= _spellHolderUI.UpdateSecondSpellCooldown;
        
        _currentPlayerSo.Player.PlayerAttackManager.FirstSpell.OnAbilityActivated -= _spellHolderUI.OnFirstSpellActivated;
        _currentPlayerSo.Player.PlayerAttackManager.SecondSpell.OnAbilityActivated -= _spellHolderUI.OnSecondSpellActivated;
    }
}
