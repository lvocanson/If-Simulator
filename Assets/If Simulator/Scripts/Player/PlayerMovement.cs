using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _drag;
    
    [ShowNonSerializedField] private Vector2 _movementValue;

    [SerializeField, BoxGroup("Inputs")] private InputActionProperty _movementInput;

    
    protected void OnEnable()
    {
        _movementInput.action.started += OnMovementAction;
        _movementInput.action.performed += OnMovementAction;
        _movementInput.action.canceled += OnMovementAction;
    }

    protected void OnDisable()
    {
        _movementInput.action.started -= OnMovementAction;
        _movementInput.action.performed -= OnMovementAction;
        _movementInput.action.canceled -= OnMovementAction;
    }

    private void FixedUpdate()
    {
        ProcessHorizontalMovement(_movementValue.x, _maxSpeed, _acceleration, _deceleration, _drag);
        ProcessVerticalMovement(_movementValue.y, _maxSpeed, _acceleration, _deceleration, _drag);
    }
    
    private void ProcessHorizontalMovement(float xMovementInput, float maxSpeed, float acceleration, float deceleration, float drag)
    {
        float scaledMaxSpeed = maxSpeed * Mathf.Abs(xMovementInput);
        float roundedXMovementInput = (Mathf.Abs(xMovementInput) > 0.1f) ? (int)Mathf.Sign(xMovementInput) : 0;

        if (roundedXMovementInput != 0) // acceleration
        {
            float targetSpeed = roundedXMovementInput * scaledMaxSpeed;
            float addedVelocity = targetSpeed * acceleration * Time.fixedDeltaTime;

            if (Mathf.Sign(_rigidbody2D.velocity.x) == roundedXMovementInput && Mathf.Abs(_rigidbody2D.velocity.x) > scaledMaxSpeed + 0.01f) // drag
            {
                float lastSign = Mathf.Sign(_rigidbody2D.velocity.x);
                float newX = Mathf.Max(Mathf.Abs(_rigidbody2D.velocity.x) - drag * maxSpeed * Time.fixedDeltaTime, scaledMaxSpeed) * lastSign; // clamp vel to max ground speed

                _rigidbody2D.velocity = new Vector2(newX, _rigidbody2D.velocity.y);
            }
            else // clamped accel
            {
                float lastSign = Mathf.Sign(_rigidbody2D.velocity.x + addedVelocity);
                float newX = Mathf.Min(Mathf.Abs(_rigidbody2D.velocity.x + addedVelocity), scaledMaxSpeed) * lastSign; // clamp vel to max ground speed

                _rigidbody2D.velocity = new Vector2(newX, _rigidbody2D.velocity.y);
            }
        }
        else // deceleration
        {
            float lastSign = Mathf.Sign(_rigidbody2D.velocity.x);
            float newX = Mathf.Max(0, Mathf.Abs(_rigidbody2D.velocity.x) - deceleration * maxSpeed * Time.fixedDeltaTime) * lastSign; // clamp vel to 0

            _rigidbody2D.velocity = new Vector2(newX, _rigidbody2D.velocity.y);
        }
    }
    private void ProcessVerticalMovement(float yMovementInput, float maxSpeed, float acceleration, float deceleration, float drag)
    {
        float scaledMaxSpeed = maxSpeed * Mathf.Abs(yMovementInput);
        float roundedYMovementInput = (Mathf.Abs(yMovementInput) > 0.1f) ? (int)Mathf.Sign(yMovementInput) : 0;

        if (roundedYMovementInput != 0) // acceleration
        {
            float targetSpeed = roundedYMovementInput * scaledMaxSpeed;
            float addedVelocity = targetSpeed * acceleration * Time.fixedDeltaTime;

            if (Mathf.Sign(_rigidbody2D.velocity.y) == roundedYMovementInput && Mathf.Abs(_rigidbody2D.velocity.y) > scaledMaxSpeed + 0.01f) // drag
            {
                float lastSign = Mathf.Sign(_rigidbody2D.velocity.y);
                float newY = Mathf.Max(Mathf.Abs(_rigidbody2D.velocity.y) - drag * maxSpeed * Time.fixedDeltaTime, scaledMaxSpeed) * lastSign; // clamp vel to max ground speed

                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, newY);
            }
            else // clamped accel
            {
                float lastSign = Mathf.Sign(_rigidbody2D.velocity.y + addedVelocity);
                float newY = Mathf.Min(Mathf.Abs(_rigidbody2D.velocity.y + addedVelocity), scaledMaxSpeed) * lastSign; // clamp vel to max ground speed

                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, newY);
            }
        }
        else // deceleration
        {
            float lastSign = Mathf.Sign(_rigidbody2D.velocity.y);
            float newY = Mathf.Max(0, Mathf.Abs(_rigidbody2D.velocity.y) - deceleration * maxSpeed * Time.fixedDeltaTime) * lastSign; // clamp vel to 0

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, newY);
        }
    }

    
    private void OnMovementAction(InputAction.CallbackContext context)
    {
        _movementValue = context.ReadValue<Vector2>();
    }
}
