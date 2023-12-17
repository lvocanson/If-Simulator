using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputMode CurrentInputMode => _currentInputMode;
    public PlayerInput PlayerInput => _playerInput;

    private InputMode _currentInputMode = InputMode.Gameplay;
    private PlayerInput _playerInput;
    private bool _isLocked = false;

    public Action<InputMode> OnInputModeChanged;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        SwitchMode(_currentInputMode);
    }

    public void SwitchMode(InputMode inputMode)
    {
        string actionMapName = GetInputModeName(inputMode);

        _currentInputMode = inputMode;
        OnInputModeChanged?.Invoke(_currentInputMode);
        if (!_isLocked) _playerInput.SwitchCurrentActionMap(actionMapName);
    }

    public void Lock()
    {
        _playerInput.DeactivateInput();
        _isLocked = true;
    }

    public void Unlock()
    {
        string actionMapName = GetInputModeName(_currentInputMode);

        _playerInput.SwitchCurrentActionMap(actionMapName);
        _isLocked = false;
    }

    private string GetInputModeName(InputMode inputMode)
    {
        return inputMode switch
        {
            InputMode.Gameplay => "Gameplay",
            InputMode.UI => "UI",
            _ => "",
        };
    }

    public enum InputMode
    {
        Gameplay,
        UI
    }
}
