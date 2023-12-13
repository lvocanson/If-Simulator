using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField, BoxGroup("Inputs")] private InputActionReference _primaryAttackInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionReference _secondaryAttackInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionReference _firstSpellInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionReference _secondSpellInput;
    
    
    protected void OnEnable()
    {
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
        if (context.started)
        {
            Debug.Log("Primary Attack Started");
        }
        else if (context.performed)
        {
            Debug.Log("Primary Attack Performed");
        }
    }
    
    private void OnSecondaryAttackAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Secondary Attack Started");
        }
    }
    
    private void OnFirstSpellAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("First Spell Started");
        }
    }
    
    private void OnSecondSpellAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Second Spell Started");
        }
    }
    
}
