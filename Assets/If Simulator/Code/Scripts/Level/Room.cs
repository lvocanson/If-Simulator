using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Level
{
    /// <summary>
    /// Abstract class that represents a room in the level, to create a new room type, create a new class that inherits from this class
    /// </summary>
    public abstract class Room : MonoBehaviour
    {
        protected enum RoomType
        {
            Base,
            KillAllEnemies,
        }

        [Header("References")]
        [SerializeField] protected List<RoomDoor> _doors;
        
        [Header("Events")]
        [SerializeField] private PhysicsEvents _enteredRoomTrigger;

        [Header("Debug")]
        [ShowNonSerializedField] protected bool _isCleared = false;
        [ShowNonSerializedField] protected bool _isActivated = false;
        [ShowNonSerializedField] protected RoomType _roomType;

        public event Action OnRoomCleared;
        public event Action OnPlayerEntered;

        
        private void OnEnable()
        {
            _enteredRoomTrigger.OnEnter += OnRoomEnter;
            _enteredRoomTrigger.OnExit += OnRoomExit;
        }

        private void OnDisable()
        {
            _enteredRoomTrigger.OnEnter -= OnRoomEnter;
            _enteredRoomTrigger.OnExit -= OnRoomExit;
        }

        public void InitializeDoors()
        {
            if (_doors.Count == 0)
            {
                Debug.LogError("Doors in room: " + gameObject.name + " isn't set");
                return;
            }
            
            foreach (var door in _doors)
            {
                if (door == null)
                {
                    Debug.LogError("A door in room: " + gameObject.name + "is null");
                    continue;
                }
                
                door.Initialize();
            }
        }
        
        private void OnRoomEnter(Collider2D other)
        {
            if (_isCleared || _isActivated || !other.CompareTag("Player")) return;
            if (!other.GetComponent<Player>()) return;
            
            OnPlayerEntered?.Invoke();
            OnPlayerEnteredRoom();
        }

        private void OnRoomExit(Collider2D other)
        {
            if (_isCleared || _isActivated || !other.CompareTag("Player")) return;
            if (!other.GetComponent<Player>()) return;
            
            OnPlayerExitedRoom();
        }

        protected void LockRoom()
        {
            Debug.Log("Room is locked");

            foreach (var door in _doors)
            {
                door.LockDoor();
            }
        }

        protected void RoomCleared()
        {
            Debug.Log("Room Cleared");
            
            OnRoomCleared?.Invoke();
            _isCleared = true;
            OnPlayerClearedRoom();
            
            UnlockRoom();
        }

        protected void UnlockRoom()
        {
            Debug.Log("Room is unlocked");

            foreach (var door in _doors)
            {
                door.UnlockDoor();
            }
        }
        
        public abstract void InitializeRoom();
        protected abstract void OnPlayerEnteredRoom();
        protected abstract void OnPlayerExitedRoom();
        protected abstract void OnPlayerClearedRoom();
    }
}
