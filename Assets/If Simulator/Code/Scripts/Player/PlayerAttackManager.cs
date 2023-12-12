using Ability;
using Ability.List;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _primaryAttackInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _secondaryAttackInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _firstSpellInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _secondSpellInput;

    private AbilityActive _primaryAttackAbilityBase;
    private AbilityActive _secondaryAttackAbilityBase;
    private AbilityActive _firstSpellAbilityBase;
    private AbilityActive _secondSpellAbilityBase;

    protected void OnEnable()
    {
        _primaryAttackAbilityBase = gameObject.AddComponent<AbilityPrimaryShoot>();
        _primaryAttackAbilityBase.AbilitySoFilePath = "PrimaryShoot"; // TODO : Find a better way to do this

        _primaryAttackInput.action.started += OnPrimaryAttackAction;
        _primaryAttackInput.action.performed += OnPrimaryAttackAction;
        _primaryAttackInput.action.canceled += OnPrimaryAttackAction;

        _secondaryAttackInput.action.started += OnSecondaryAttackAction;
        //_secondaryAttackInput.action.performed += OnSecondaryAttackAction;
        _secondaryAttackInput.action.canceled += OnSecondaryAttackAction;

        _firstSpellInput.action.started += OnFirstSpellAction;
        //_firstSpellInput.action.performed += OnFirstSpellAction;
        _firstSpellInput.action.canceled += OnFirstSpellAction;

        _secondSpellInput.action.started += OnSecondSpellAction;
        //_secondSpellInput.action.performed += OnSecondSpellAction;
        _secondSpellInput.action.canceled += OnSecondSpellAction;
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
        if (context is { started: false, performed: false }) return;

        _primaryAttackAbilityBase.TryActivate();
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