using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

public class PlayerRotation : MonoBehaviour
{
    [ShowNonSerializedField] private Vector2 _mousePosition;
    private Camera _mainCamera;
    
    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _rotationInput;

    
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    
    private void OnEnable()
    {
        _rotationInput.action.started += OnRotationAction;
        _rotationInput.action.performed += OnRotationAction;
        _rotationInput.action.canceled += OnRotationAction;
    }

    private void OnDisable()
    {
        _rotationInput.action.started -= OnRotationAction;
        _rotationInput.action.performed -= OnRotationAction;
        _rotationInput.action.canceled -= OnRotationAction;
    }

    private void FixedUpdate()
    {
        RotateAim();
    }

    private void RotateAim()
    {
        Vector2 positionOnScreen = _mainCamera.WorldToScreenPoint(transform.position);

        float angle = TransformUtility.AngleBetweenTwoPoints(positionOnScreen, _mousePosition);

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    private void OnRotationAction(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }
}
