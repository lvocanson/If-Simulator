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
    }

    private void OnPrimaryAttackEndAction(InputAction.CallbackContext context)
    {
        _primaryAttackAbilityBase.End();
    }

    private void OnSecondaryAttackAction(InputAction.CallbackContext context)
    {
        _secondaryAttackAbilityBase.TryActivate();
    }
    
    private void OnDashAction(InputAction.CallbackContext context)
    {
        _dashAbilityBase.TryActivate();
    }

    private void OnFirstSpellAction(InputAction.CallbackContext context)
    {
        _firstSpellAbilityBase.TryActivate();
    }

    private void OnSecondSpellAction(InputAction.CallbackContext context)
    {
        _secondSpellAbilityBase.TryActivate();
    }
}