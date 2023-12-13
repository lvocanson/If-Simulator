using GameMode;
using Managers;
using UnityEngine;

public class LevelManager : InGameManager
{
    [SerializeField] private Level _currentLevel;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private CurrentPlayerSo _currentPlayerSo;
    
    // TODO: Change type to Player when merged with Arthur
    private PlayerMovement _spawnedPlayer;
    
    public PlayerMovement SpawnedPlayer => _spawnedPlayer;
    public CurrentPlayerSo CurrentPlayerSo => _currentPlayerSo;

    protected override void OnContextInitialized(GameModeStartMode mode)
    {
        _currentLevel.Initialize();
    }

    protected override void OnContextStarted(GameModeStartMode mode)
    {
        _spawnedPlayer = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity).GetComponent<PlayerMovement>();
        _currentPlayerSo.Load(_spawnedPlayer);
    }

    protected override void OnContextQuit(GameModeQuitMode mode)
    {
        
    }
}
