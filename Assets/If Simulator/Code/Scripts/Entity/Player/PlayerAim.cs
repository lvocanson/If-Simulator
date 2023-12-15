using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform _bodyTransform;
    [SerializeField] private GameObject _cursorPrefab;
    
    [SerializeField, BoxGroup("Inputs")] private InputActionReference _rotationInput;
    
    public GameObject AimCursor => _aimCursor;
    
    [ShowNonSerializedField] private Vector2 _mousePosition;
    private Camera _mainCamera;
    private GameObject _aimCursor;
    
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _aimCursor = Instantiate(_cursorPrefab, transform);
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

    private void Update()
    {
        RotateAim();
        MoveCursor();
    }

    private void RotateAim()
    {
        Vector2 positionOnScreen = _mainCamera.WorldToScreenPoint(transform.position);

        float angle = TransformUtility.AngleBetweenTwoPoints(positionOnScreen, _mousePosition);

        _bodyTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    private void OnRotationAction(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    private void MoveCursor()
    {
        _aimCursor.transform.position = (Vector2)_mainCamera.ScreenToWorldPoint(_mousePosition);;
    }
}
