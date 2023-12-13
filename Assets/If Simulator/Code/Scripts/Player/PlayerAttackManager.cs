using Ability;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _primaryAttackInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _secondaryAttackInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _firstSpellInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _secondSpellInput;

    [SerializeField] private AbilityActive _primaryAttackAbilityBase;
    [SerializeField] private AbilityActive _secondaryAttackAbilityBase;
    [SerializeField] private AbilityActive _firstSpellAbilityBase;
    [SerializeField] private AbilityActive _secondSpellAbilityBase;

    protected void OnEnable()
    {
        _primaryAttackInput.action.started += OnPrimaryAttackAction;
        _primaryAttackInput.action.canceled += OnPrimaryAttackEndAction;

        _secondaryAttackInput.action.started += OnSecondaryAttackAction;

        _firstSpellInput.action.started += OnFirstSpellAction;

        _secondSpellInput.action.started += OnSecondSpellAction;
    }

    protected void OnDisable()
    {
        _primaryAttackInput.action.started -= OnPrimaryAttackAction;
        _primaryAttackInput.action.performed -= OnPrimaryAttackAction;
        _primaryAttackInput.action.canceled -= OnPrimaryAttackAction;

        _secondaryAttackInput.action.started -= OnSecondaryAttackAction;
        //_secondaryAttackInput.action.performed -= OnSecondaryAttackAction;
        _secondaryAttackInput.action.canceled -= OnSecondaryAttackAction;

        _firstSpellInput.action.started -= OnFirstSpellAction;
        //_firstSpellInput.action.performed -= OnFirstSpellAction;
        _firstSpellInput.action.canceled -= OnFirstSpellAction;

        _secondSpellInput.action.started -= OnSecondSpellAction;
        //_secondSpellInput.action.performed -= OnSecondSpellAction;
        _secondSpellInput.action.canceled -= OnSecondSpellAction;
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
        if (context.started)
        {
            _secondaryAttackAbilityBase.TryActivate();
        }
    }

    private void OnFirstSpellAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _firstSpellAbilityBase.TryActivate();
        }
    }

    private void OnSecondSpellAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _secondSpellAbilityBase.TryActivate();
        }
    }
}