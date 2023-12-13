using GameMode;
using Managers;
using UnityEngine;

public class LevelManager : InGameManager
{
    [SerializeField] private Level _currentLevel;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private CurrentPlayerSo _currentPlayerSo;
    
    private Player _spawnedPlayer;
    
    public Player SpawnedPlayer => _spawnedPlayer;
    public CurrentPlayerSo CurrentPlayerSo => _currentPlayerSo;

    protected override void OnContextInitialized(GameModeStartMode mode)
    {
        _currentLevel.Initialize();
    }

    protected override void OnContextStarted(GameModeStartMode mode)
    {
        _spawnedPlayer = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity).GetComponentInChildren<Player>();
        _currentPlayerSo.Load(_spawnedPlayer);
    }

    protected override void OnContextQuit(GameModeQuitMode mode)
    {
        
    }
}
