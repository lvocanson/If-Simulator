using System;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class App : MonoBehaviour
{
    #region Singleton
    public static App Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            Load();
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

    public GameModeManager GameMode { get; private set; }
    public BackdropManager Backdrop { get; private set; }
    public PlayerInput Input { get; private set; }

    public static InputManager InputManager => Instance._inputManager;

    private InputManager _inputManager;

    private void Load()
    {
        Random.InitState((int)DateTime.Now.Ticks);

        GameMode = GetComponentInChildren<GameModeManager>();
        Backdrop = GetComponentInChildren<BackdropManager>();
        Input = GetComponentInChildren<PlayerInput>();
        _inputManager = GetComponentInChildren<InputManager>();

        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 0;
    }
}
