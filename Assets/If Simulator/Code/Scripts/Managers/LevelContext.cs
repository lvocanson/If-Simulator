using System;
using GameMode;
using UnityEngine;

public class LevelContext : MonoBehaviour
{
    public static LevelContext Instance { get; private set; }
    
    public LevelManager LevelManager => Instance._levelManager;
    public CameraManager CameraManager => Instance._cameraManager;

    [Header("Managers")]
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private CameraManager _cameraManager;

    
    [Header("Events")]
    [SerializeField] private EventSo _levelContextInitialized;
    [SerializeField] private EventSo _levelContextStarted;
    
    
    public event Action<GameModeStartMode> OnStarted;
    public event Action<GameModeStartMode> OnInitialized;
    public event Action<GameModeQuitMode> OnQuit;
    
    
    private GameModeStartMode _currentStartMode;
    public GameModeStartMode CurrentStartMode => _currentStartMode;
    
    
    #region Singleton 
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
    
    #endregion
    
    public void InitializeContext(GameModeStartMode startMode)
    {
        _currentStartMode = startMode;
        
        OnInitialized?.Invoke(_currentStartMode);
        _levelContextInitialized.Invoke();
    }

    public void StartContext()
    {
        OnStarted?.Invoke(_currentStartMode);
        _levelContextStarted.Invoke();
    }

    public void QuitContext(GameModeQuitMode quitMode)
    {
        OnQuit?.Invoke(quitMode);
    }
}
