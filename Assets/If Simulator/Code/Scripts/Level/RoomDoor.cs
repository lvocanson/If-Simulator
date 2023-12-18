using NaughtyAttributes;
using UnityEngine;


public enum DoorState
{
    Opened,
    Unlocked,
    Locked
}

public class RoomDoor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _renderer;

    [Header("Settings")]
    [SerializeField] private DoorState _initialState = DoorState.Unlocked;

    [Header("Sprites")]
    /* TODO : Replace color by sprites
    [SerializeField] private Sprite _openedSprite;
    [SerializeField] private Sprite _closedSprite;
    [SerializeField] private Sprite _lockedSprite;
    */
    [SerializeField] private Color _openedColor;
    [SerializeField] private Color _unlockedColor;
    [SerializeField] private Color _lockedColor;

    [Header("Debug")]
    [ShowNonSerializedField] private DoorState _currentState = DoorState.Unlocked;
    [ShowNonSerializedField] private bool _isAlreadyOpened = false;

    private DoorState _previousState;

    public DoorState CurrentState => _currentState;
    public DoorState PreviousState => _previousState;

    public void Initialize()
    {
        _currentState = _initialState;
        _previousState = _currentState;

        switch (_currentState)
        {
            case DoorState.Locked:
                LockDoor();
                break;
            case DoorState.Unlocked:
                UnlockDoor();
                break;
            case DoorState.Opened:
                OpenDoor();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && _currentState == DoorState.Unlocked)
            OpenDoor();
    }

    public void OpenDoor()
    {
        _currentState = DoorState.Opened;
        _isAlreadyOpened = true;

        _renderer.color = _openedColor;
        _collider.enabled = false;
        _renderer.enabled = false;
    }

    public void LockDoor()
    {
        _previousState = _currentState;
        _currentState = DoorState.Locked;

        _renderer.color = _lockedColor;
        _collider.enabled = true;
        _renderer.enabled = true;
    }

    public void UnlockDoor()
    {
        _currentState = DoorState.Unlocked;

        if (_isAlreadyOpened)
            OpenDoor();
        else
        {
            _renderer.color = _unlockedColor;
            _collider.enabled = true;
            _renderer.enabled = true;
        }
    }
}
