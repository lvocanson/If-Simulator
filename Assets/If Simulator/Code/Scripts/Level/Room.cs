using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private enum RoomType
    {
        Basic,
        KillAllEnemies,
    }
    
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<RoomDoor> _doors;
    [SerializeField] private RoomType _roomType = RoomType.Basic;
    
    public event Action OnRoomCleared;
    
    
    public void Initialize()
    {
        foreach (var door in _doors)
        {
            door.Initialize();
            door.OnPlayerEnteredRoom += ActivateRoom;
        }
    }
    
    private void ActivateRoom()
    {
        switch (_roomType)
        {
            case RoomType.Basic:
                break;
            case RoomType.KillAllEnemies:
                
                foreach (var enemy in _enemies)
                {
                    enemy.OnDeath += CheckEnemies;
                }

                foreach (var door in _doors)
                {
                    door.LockDoor();
                }
                
                OnRoomCleared += () =>
                {
                    foreach (var door in _doors)
                    {
                        door.UnlockDoor();
                    }
                };
                
                break;
        }
    }
    
    private void CheckEnemies()
    {
        if (_enemies.Count > 0) return;

        OnRoomCleared?.Invoke();
    }
}
