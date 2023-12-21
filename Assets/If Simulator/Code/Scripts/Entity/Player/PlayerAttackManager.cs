using System;
using System.Linq;
using Ability;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField, BoxGroup("Inputs")] private InputActionReference _primaryAttackInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionReference _secondaryAttackInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionReference _dashInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionReference _firstSpellInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionReference _secondSpellInput;
    
    public event Action<AbilityActive> OnAbilityActivated;
    public event Action<AbilityActive> OnFirstSpellChanged;
    public event Action<AbilityActive> OnSecondSpellChanged;
    
    public AbilityActive FirstSpell => _firstSpellAbilityBase;
    public AbilityActive SecondSpell => _secondSpellAbilityBase;

    [Header("References")]
    [SerializeField] private GameObject _spellsGo;
    [SerializeField] private PlayerXp _playerXp;
    
    [Header("Spells")]
    [SerializeField] private AbilityShoot _primaryAttackAbilityBase;
    [SerializeField] private AbilityShoot _secondaryAttackAbilityBase;
    [SerializeField] private DashBehavior _dashAbilityBase;
    private AbilityActive _firstSpellAbilityBase;
    private AbilityActive _secondSpellAbilityBase;
    
    public void ChangeFirstSpell(SoAbilityBase newSpell)
    {
        var spells = _spellsGo.GetComponents<AbilityActive>();
        AbilityActive s = spells.First(e => e.CompareAbility(newSpell));
        if (s == null)
        {
            Debug.LogError("Spell not found", this);
        }
        
        if(_firstSpellAbilityBase != null) 
            _firstSpellAbilityBase.OnEnemyKilled -= _playerXp.AddXp;
        
        _firstSpellAbilityBase = s;
        _firstSpellAbilityBase.OnEnemyKilled += _playerXp.AddXp;
        
        OnFirstSpellChanged?.Invoke(_firstSpellAbilityBase);
    }
    
    public void ChangeSecondSpell(SoAbilityBase newSpell)
    {
        var spells = _spellsGo.GetComponents<AbilityActive>();
        AbilityActive s = spells.First(e => e.CompareAbility(newSpell));
        if (s == null)
        {
            Debug.LogError("Spell not found", this);
        }
        
        if(_secondSpellAbilityBase != null) 
            _secondSpellAbilityBase.OnEnemyKilled -= _playerXp.AddXp;
        
        _secondSpellAbilityBase = s;
        _secondSpellAbilityBase.OnEnemyKilled += _playerXp.AddXp;
        
        OnSecondSpellChanged?.Invoke(_secondSpellAbilityBase);
    }
    
    public SoAbilityBase GetFirstSpell()
    {
        return _firstSpellAbilityBase.RuntimeAbilitySo;
    }
    
    public SoAbilityBase GetSecondSpell()
    {
        return _secondSpellAbilityBase.RuntimeAbilitySo;
    }
    
    protected void OnEnable()
    {
        _primaryAttackInput.action.started += OnPrimaryAttackAction;
        _primaryAttackInput.action.canceled += OnPrimaryAttackEndAction;

        _secondaryAttackInput.action.started += OnSecondaryAttackAction;
        _secondaryAttackInput.action.canceled += OnSecondaryAttackEndAction;
        
        _dashInput.action.started += OnDashAction;

        _firstSpellInput.action.started += OnFirstSpellAction;

        _secondSpellInput.action.started += OnSecondSpellAction;
    }

    protected void OnDisable()
    {
        _primaryAttackInput.action.started -= OnPrimaryAttackAction;
        _primaryAttackInput.action.canceled -= OnPrimaryAttackAction;

        _secondaryAttackInput.action.started -= OnSecondaryAttackAction;
        _secondaryAttackInput.action.canceled -= OnSecondaryAttackEndAction;
        
        _dashInput.action.started -= OnDashAction;

        _firstSpellInput.action.started -= OnFirstSpellAction;

        _secondSpellInput.action.started -= OnSecondSpellAction;
    }

    private void Start()
    {
        _primaryAttackAbilityBase.OnEnemyKilled += _playerXp.AddXp;
        _secondaryAttackAbilityBase.OnEnemyKilled += _playerXp.AddXp;
    }

    private void OnPrimaryAttackAction(InputAction.CallbackContext context)
    {
        _primaryAttackAbilityBase.TryActivate();
        OnAbilityActivated?.Invoke(_primaryAttackAbilityBase);
    }

    private void OnPrimaryAttackEndAction(InputAction.CallbackContext context)
    {
        _primaryAttackAbilityBase.End();
    }

    private void OnSecondaryAttackAction(InputAction.CallbackContext context)
    {
        _secondaryAttackAbilityBase.TryActivate();
        OnAbilityActivated?.Invoke(_secondaryAttackAbilityBase);
    }
    
    private void OnSecondaryAttackEndAction(InputAction.CallbackContext context)
    {
        _secondaryAttackAbilityBase.End();
    }
    
    private void OnDashAction(InputAction.CallbackContext context)
    {
        _dashAbilityBase.TryActivate();
        OnAbilityActivated?.Invoke(_dashAbilityBase);
    }

    private void OnFirstSpellAction(InputAction.CallbackContext context)
    {
        _firstSpellAbilityBase.TryActivate();
        OnAbilityActivated?.Invoke(_firstSpellAbilityBase);
    }

    private void OnSecondSpellAction(InputAction.CallbackContext context)
    {
        _secondSpellAbilityBase.TryActivate();
        OnAbilityActivated?.Invoke(_secondSpellAbilityBase);
    }
}