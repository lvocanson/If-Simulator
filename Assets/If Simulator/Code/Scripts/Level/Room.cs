using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Room : MonoBehaviour
{
    private enum RoomType
    {
        Basic,
        KillAllEnemies,
    }
    
    [Header("References")]
    [SerializeField] private List<Enemy> _allEnemies;
    [SerializeField] private List<RoomDoor> _doors;
    
    [Header("Room Settings")]
    [SerializeField] private RoomType _roomType = RoomType.Basic;
    
    [Header("Events")]
    [SerializeField] private PhysicsEvents _enteredRoomTrigger;
    
    [Header("Debug")]
    [ShowNonSerializedField] private bool _isCleared = false;
    [ShowNonSerializedField] private bool _isActivated = false;
    [ShowNonSerializedField] private int _enemiesAlive = 0;
    [ShowNonSerializedField] private int _totalEnemies = 0;

    public event Action OnRoomCleared;


    private void OnEnable()
    {
        _enteredRoomTrigger.OnEnter += OnRoomEnter;
        _enteredRoomTrigger.OnExit += OnRoomExit;
    }

    private void OnDisable()
    {
        _enteredRoomTrigger.OnEnter -= OnRoomEnter;
        _enteredRoomTrigger.OnExit -= OnRoomExit;
    }

    public void Initialize()
    {
        if (_roomType is RoomType.Basic) _isCleared = true;
        
        foreach (var door in _doors)
        {
            door.Initialize();
        }
        
        foreach (var enemy in _allEnemies)
        {
            if (!enemy) continue;
            
            enemy.OnDeath += OnEnemyKilled;
            _enemiesAlive++;
        }
        
        _totalEnemies = _enemiesAlive;
    }
    
    private void OnRoomEnter(Collider2D other)
    {
        if (_isCleared || _isActivated || !other.CompareTag("Player")) return;
        if (!other.GetComponent<Player>()) return;

        Debug.Log("Player entered room: " + other.name);
        switch (_roomType)
        {
            case RoomType.Basic:
                break;
            case RoomType.KillAllEnemies:
                ActivateRoom();
                break;
        }
    }
    
    private void OnRoomExit(Collider2D other)
    {
        if (_isCleared || _isActivated || !other.CompareTag("Player")) return;
        if (!other.GetComponent<Player>()) return;
            
        switch (_roomType)
        {
            case RoomType.Basic:
                break;
            case RoomType.KillAllEnemies:
                break;
        }
    }
    
    private void ActivateRoom()
    {
        Debug.Log("Room is activated");
        
        _isActivated = true;
        
        switch (_roomType)
        {
            case RoomType.Basic:
                break;
            case RoomType.KillAllEnemies:

                LockRoom();
                
                break;
        }
    }

    private void LockRoom()
    {
        Debug.Log("Room is locked");

        foreach (var door in _doors)
        {
            door.LockDoor();
        }
    }
    
    private void RoomCleared()
    {
        Debug.Log("Room Cleared");
        
        _isCleared = true;
        UnlockRoom();
        OnRoomCleared?.Invoke();
    }
    
    private void UnlockRoom()
    {
        Debug.Log("Room is unlocked");

        foreach (var door in _doors)
        {
            door.UnlockDoor();
        }
    }


    private void OnEnemyKilled()
    {
        _enemiesAlive--;
        Debug.Log("Enemy killed: " + _enemiesAlive + " enemies left");

        if (_enemiesAlive > 0) return;

        RoomCleared();
    }
}
