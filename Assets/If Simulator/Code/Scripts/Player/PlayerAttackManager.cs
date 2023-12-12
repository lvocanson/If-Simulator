using System.Collections.Generic;
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
    
    private Ability.AbilityActive _primaryAttackAbility;
    private Ability.AbilityActive _secondaryAttackAbility;
    private Ability.AbilityActive _firstSpellAbility;
    private Ability.AbilityActive _secondSpellAbility;

    private List<AbilityPassive> _abilityPassives;
    
    protected void OnEnable()
    {
        _primaryAttackAbility = gameObject.AddComponent<AbilityPrimaryShoot>();
        _primaryAttackAbility.AbilitySoFilePath = "PrimaryShoot"; // TODO : Find a better way to do this
            
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
        
        _primaryAttackAbility.TryActivate();
    }
    
    private void OnSecondaryAttackAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _secondaryAttackAbility.TryActivate();
        }
    }
    
    private void OnFirstSpellAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _firstSpellAbility.TryActivate();
        }
    }
    
    private void OnSecondSpellAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _secondSpellAbility.TryActivate();
        }
    }
    
}
