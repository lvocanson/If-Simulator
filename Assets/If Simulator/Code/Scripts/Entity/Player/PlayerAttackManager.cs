using System;
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

    [SerializeField] private AbilityActive _primaryAttackAbilityBase;
    [SerializeField] private AbilityActive _secondaryAttackAbilityBase;
    [SerializeField] private DashBehavior _dashAbilityBase;
    [SerializeField] private AbilityActive _firstSpellAbilityBase;
    [SerializeField] private AbilityActive _secondSpellAbilityBase;

    protected void OnEnable()
    {
        _primaryAttackInput.action.started += OnPrimaryAttackAction;
        _primaryAttackInput.action.canceled += OnPrimaryAttackEndAction;

        _secondaryAttackInput.action.started += OnSecondaryAttackAction;
        
        _dashInput.action.started += OnDashAction;

        _firstSpellInput.action.started += OnFirstSpellAction;

        _secondSpellInput.action.started += OnSecondSpellAction;
    }

    protected void OnDisable()
    {
        _primaryAttackInput.action.started -= OnPrimaryAttackAction;
        _primaryAttackInput.action.canceled -= OnPrimaryAttackAction;

        _secondaryAttackInput.action.started -= OnSecondaryAttackAction;
        
        _dashInput.action.started -= OnDashAction;

        _firstSpellInput.action.started -= OnFirstSpellAction;

        _secondSpellInput.action.started -= OnSecondSpellAction;
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