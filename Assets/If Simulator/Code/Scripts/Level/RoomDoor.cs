using System;
using NaughtyAttributes;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    private enum DoorState
    {
        Opened,
        Unlocked,
        Locked,
        Closed
    }
    
    [Header("References")]
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _renderer;
    
    [Header("Settings")]
    [SerializeField] private DoorState _initialState = DoorState.Unlocked;
    
    [Header("Debug")]
    [ShowNonSerializedField] private DoorState _currentState = DoorState.Unlocked;
    

    public void Initialize()
    {
        _currentState = _initialState;

        switch (_currentState)
        {
            case DoorState.Locked:
                LockDoor();
                break;
            case DoorState.Unlocked:
                UnlockDoor();
                break;
            case DoorState.Closed:
                CloseDoor();
                break;
            case DoorState.Opened:
                OpenDoor();
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || _currentState is not DoorState.Unlocked) return;
        
        OpenDoor();
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || _currentState is not DoorState.Opened) return;
    }

    public void OpenDoor()
    {
        _currentState = DoorState.Opened;
        _collider.enabled = false;
        _renderer.enabled = false;
    }
    
    public void CloseDoor()
    {
        _currentState = DoorState.Closed;
        _collider.enabled = true;
        _renderer.enabled = true;
    }
    
    public void LockDoor()
    {
        _currentState = DoorState.Locked;
        _collider.enabled = true;
        _renderer.enabled = true;
    }
    
    public void UnlockDoor()
    {
        _currentState = DoorState.Unlocked;
        _collider.enabled = true;
        _renderer.enabled = true;
    }
}
