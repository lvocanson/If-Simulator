using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private enum RoomType
    {
        Basic,
        KillAllEnemies,
    }
    
    [SerializeField] private List<RoomDoor> _doors;
    [SerializeField] private RoomType _roomType = RoomType.Basic;
    
    public void Initialize()
    {
        foreach (var door in _doors)
        {
            door.Initialize();
        }
    }
}
