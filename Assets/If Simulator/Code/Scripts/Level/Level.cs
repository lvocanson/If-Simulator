using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Room> _rooms;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    
    
    public void Initialize()
    {
        foreach (var room in _rooms)
        {
            room.Initialize();
        }
    }
    
    public void UpdateNavMesh()
    {
        _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
    }
}
