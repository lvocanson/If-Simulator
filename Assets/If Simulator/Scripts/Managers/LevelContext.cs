using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContext : MonoBehaviour
{
    public static LevelContext Instance { get; private set; }
    
    public LevelManager LevelManager => Instance._levelManager;
    
    [Header("Managers")]
    [SerializeField] private LevelManager _levelManager;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
