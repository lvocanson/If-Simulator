using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _primaryAttackInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _secondaryAttackInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _firstSpellInput;
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _secondSpellInput;
    
    
    protected void OnEnable()
    {
        _primaryAttackInput.action.started += OnPrimaryAttackAction;
        _primaryAttackInput.action.performed += OnPrimaryAttackAction;
        _primaryAttackInput.action.canceled += OnPrimaryAttackAction;
        
        _secondaryAttackInput.action.started += OnPrimaryAttackAction;
        _secondaryAttackInput.action.performed += OnPrimaryAttackAction;
        _secondaryAttackInput.action.canceled += OnPrimaryAttackAction;
    }

    protected void OnDisable()
    {
        _primaryAttackInput.action.started -= OnPrimaryAttackAction;
        _primaryAttackInput.action.performed -= OnPrimaryAttackAction;
        _primaryAttackInput.action.canceled -= OnPrimaryAttackAction;
        
        _secondaryAttackInput.action.started -= OnPrimaryAttackAction;
        _secondaryAttackInput.action.performed -= OnPrimaryAttackAction;
        _secondaryAttackInput.action.canceled -= OnPrimaryAttackAction;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        else if (context.canceled)
        {
            Debug.Log("Primary Attack Canceled");
        }
    }
    
}
