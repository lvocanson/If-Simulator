using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Room> _rooms;
    
    public void Initialize()
    {
        foreach (var room in _rooms)
        {
            room.Initialize();
        }
    }
}
