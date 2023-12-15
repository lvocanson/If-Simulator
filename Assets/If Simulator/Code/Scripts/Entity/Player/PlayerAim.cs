using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _bodyRigidbody2D;
    [SerializeField] private GameObject _cursorPrefab;
    
    [SerializeField, BoxGroup("Inputs")] private InputActionReference _rotationInput;
    
    public GameObject AimCursor => _aimCursor;
    
    [ShowNonSerializedField] private Vector2 _mousePosition;
    
    private Camera _mainCamera;
    private GameObject _aimCursor;
    

    private void Awake()
    {
        _mainCamera = LevelContext.Instance.CameraManager.MainCamera;
        _mousePosition = _mainCamera.WorldToScreenPoint(transform.position);
        _aimCursor = Instantiate(_cursorPrefab, transform.position, Quaternion.identity);
    }

    private void Start()
    {
        
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

    private void Update()
    {
        MoveCursor();
    }

    private void RotateAim()
    {
        Vector2 positionOnScreen = _mainCamera.WorldToScreenPoint(transform.position);

        float angle = TransformUtility.AngleBetweenTwoPoints(positionOnScreen, _mousePosition);
        
        _bodyRigidbody2D.MoveRotation(Quaternion.Euler(new Vector3(0f, 0f, angle)));
    }

    private void OnRotationAction(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    private void MoveCursor()
    {
        _aimCursor.transform.position = (Vector2)_mainCamera.ScreenToWorldPoint(_mousePosition);
    }
}
