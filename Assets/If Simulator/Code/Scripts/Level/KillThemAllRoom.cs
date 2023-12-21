using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Level
{
    public class KillThemAllRoom : Room
    {
        [Header("Kill Them All Room")]
        [SerializeField] private List<Enemy> _allEnemies;
        
        [ShowNonSerializedField] private int _enemiesAlive = 0;
        [ShowNonSerializedField] private int _totalEnemies = 0;


        public override void InitializeRoom()
        {
            _roomType = RoomType.KillAllEnemies;
            
            foreach (var enemy in _allEnemies)
            {
                if (!enemy) continue;

                enemy.OnDeath += OnEnemyKilled;
                _enemiesAlive++;
            }
            
            _totalEnemies = _enemiesAlive;
        }

        protected override void OnPlayerEnteredRoom()
        {
            if (_enemiesAlive > 0)
                LockRoom();

            _isActivated = true;
        }

        protected override void OnPlayerExitedRoom()
        {
            
        }

        protected override void OnPlayerClearedRoom()
        {
            
        }

        private void OnEnemyKilled()
        {
            if (--_enemiesAlive == 0)
                RoomCleared();

            Debug.Log("Enemy killed: " + _enemiesAlive + " enemies left");
        }
    }
}