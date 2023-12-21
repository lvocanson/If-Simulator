using System;
using System.Linq;
using Ability;
using JetBrains.Annotations;
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
        if (newSpell == null) return;
        
        var spells = _spellsGo.GetComponents<AbilityActive>();
        AbilityActive s = spells.First(e => e.CompareAbility(newSpell));
        if (s == null) Debug.LogError("Spell not found", this);
        
        if (_firstSpellAbilityBase != null)
        {
            if (newSpell.Name == _firstSpellAbilityBase.RuntimeAbilitySo.Name)
            {
                Debug.Log("Level up : " + _firstSpellAbilityBase.RuntimeAbilitySo.Name);
                _firstSpellAbilityBase.LevelUp();
                OnFirstSpellChanged?.Invoke(_firstSpellAbilityBase);
                return;
            }
            _firstSpellAbilityBase.OnEnemyKilled -= _playerXp.AddXp;
        }
        
        _firstSpellAbilityBase = s;
        _firstSpellAbilityBase.OnEnemyKilled += _playerXp.AddXp;
        
        OnFirstSpellChanged?.Invoke(_firstSpellAbilityBase);
    }
    
    public void ChangeSecondSpell(SoAbilityBase newSpell)
    {
        if (newSpell == null) return;

        var spells = _spellsGo.GetComponents<AbilityActive>();
        AbilityActive s = spells.First(e => e.CompareAbility(newSpell));
        if (s == null) Debug.LogError("Spell not found", this);
        
        if (_secondSpellAbilityBase != null)
        {
            if (newSpell.Name == _secondSpellAbilityBase.RuntimeAbilitySo.Name)
            {
                _secondSpellAbilityBase.LevelUp();
                OnSecondSpellChanged?.Invoke(_secondSpellAbilityBase);
                return;
            }
            _secondSpellAbilityBase.OnEnemyKilled -= _playerXp.AddXp;
        }
        
        _secondSpellAbilityBase = s;
        _secondSpellAbilityBase.OnEnemyKilled += _playerXp.AddXp;
        
        OnSecondSpellChanged?.Invoke(_secondSpellAbilityBase);
    }
    
    [CanBeNull]
    public SoAbilityBase GetFirstSpell()
    {
        return _firstSpellAbilityBase == null ? null : _firstSpellAbilityBase.RuntimeAbilitySo;
    }
    
    [CanBeNull]
    public SoAbilityBase GetSecondSpell()
    {
        return _secondSpellAbilityBase == null ? null : _secondSpellAbilityBase.RuntimeAbilitySo;
    }
    
    public void ResetFirstSpell()
    {
        _firstSpellAbilityBase.ResetAbility();
    }
    
    public void ResetSecondSpell()
    {
        _secondSpellAbilityBase.ResetAbility();
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
        if (_primaryAttackAbilityBase)
        {
            _primaryAttackAbilityBase.TryActivate();
            OnAbilityActivated?.Invoke(_primaryAttackAbilityBase);
        }
    }

    private void OnPrimaryAttackEndAction(InputAction.CallbackContext context)
    {
        if (_primaryAttackAbilityBase)
        {
            _primaryAttackAbilityBase.End();
        }
    }

    private void OnSecondaryAttackAction(InputAction.CallbackContext context)
    {
        if (_secondaryAttackAbilityBase)
        {
            _secondaryAttackAbilityBase.TryActivate();
            OnAbilityActivated?.Invoke(_secondaryAttackAbilityBase);
        }
    }
    
    private void OnSecondaryAttackEndAction(InputAction.CallbackContext context)
    {
        if (_secondaryAttackAbilityBase)
        {
            _secondaryAttackAbilityBase.End();
        }
    }
    
    private void OnDashAction(InputAction.CallbackContext context)
    {
        _dashAbilityBase.TryActivate();
        OnAbilityActivated?.Invoke(_dashAbilityBase);
    }

    private void OnFirstSpellAction(InputAction.CallbackContext context)
    {
        if (_firstSpellAbilityBase)
        {
            _firstSpellAbilityBase.TryActivate();
            OnAbilityActivated?.Invoke(_firstSpellAbilityBase);
        }
    }

    private void OnSecondSpellAction(InputAction.CallbackContext context)
    {
        if (_secondSpellAbilityBase)
        {
            _secondSpellAbilityBase.TryActivate();
            OnAbilityActivated?.Invoke(_secondSpellAbilityBase);
        }
    }
}