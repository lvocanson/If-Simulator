using System;
using Game.Level;
using GameMode;
using IfSimulator.GOAP.Behaviors;
using UnityEngine;

namespace Managers
{
    public class LevelManager : InGameManager
    {
        [SerializeField] private Level _currentLevel;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _allyPrefab;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private CurrentPlayerSo _currentPlayerSo;

        private Player _spawnedPlayer;
        private AllyBrain _spawnedAlly;

        public Player SpawnedPlayer => _spawnedPlayer;
        public Level CurrentLevel => _currentLevel;
        public CurrentPlayerSo CurrentPlayerSo => _currentPlayerSo;
        
        
        public event Action OnRestart;

        
        protected override void OnContextInitialized(GameModeStartMode mode)
        {
            if (_currentLevel)
                _currentLevel.Initialize();
        }
        
        protected override void OnContextStarted(GameModeStartMode mode)
        {
            SpawnPlayer();
        }
        
        private void SpawnPlayer()
        {
            _spawnedPlayer = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity).GetComponentInChildren<Player>();
            _spawnedAlly = Instantiate(_allyPrefab, _spawnedPlayer.AllyTarget.position, Quaternion.identity).GetComponent<AllyBrain>();
            _spawnedAlly.SetPlayer(_spawnedPlayer);
            
            foreach (var room in _currentLevel.Rooms)
            {
                room.OnPlayerEntered += _spawnedAlly.TeleportToPlayer;
            }
        }

        protected override void OnContextQuit(GameModeQuitMode mode)
        {
            foreach (var room in _currentLevel.Rooms)
            {
                room.OnPlayerEntered -= _spawnedAlly.TeleportToPlayer;
            }
        }
        
        
        
        public void RestartLevel()
        {
            OnRestart?.Invoke();
            App.Instance.GameMode.ReloadCurrentMode();
        }
    }
}
