using Cinemachine;
using GameMode;
using UnityEngine;

namespace Managers
{
    public class CameraManager : InGameManager
    {
        [SerializeField] private CinemachineVirtualCamera _currentCamera;
        [SerializeField] private CinemachineTargetGroup _targetGroup;
        [SerializeField] private CurrentPlayerSo _currentPlayerSo;
    
        private Camera _mainCamera;
        
        public Camera MainCamera => _mainCamera;
        

        private void OnEnable()
        {
            _currentPlayerSo.OnPlayerLoaded += AddPlayerAsTarget;
        }
    
        private void OnDisable()
        {
            _currentPlayerSo.OnPlayerLoaded -= AddPlayerAsTarget;
        }
    
        protected override void OnContextInitialized(GameModeStartMode mode)
        {
            _mainCamera = Camera.main;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    
        protected override void OnContextStarted(GameModeStartMode mode)
        {
            
        }
    
        protected override void OnContextQuit(GameModeQuitMode mode)
        {
            Debug.Log("CameraManager.OnContextQuit");
            ClearTargets();
        }
    
        private void AddPlayerAsTarget()
        {
            if (_targetGroup != null)
            {
                AddTarget(_currentPlayerSo.Player.transform, 1, 7);
                AddTarget(_currentPlayerSo.Player.PlayerAim.AimCursor.transform, 0.6f, 1);
            }
            else
            {
                _currentCamera.Follow = _currentPlayerSo.Player.transform;
            }
        }
    
        public void AddTarget(Transform target, float weight, float radius)
        {
            _targetGroup.AddMember(target, weight, radius);
        }
    
        public void RemoveTarget(Transform target)
        {
            _targetGroup.RemoveMember(target);
        }
        
        public void ClearTargets()
        {
            foreach (var target in _targetGroup.m_Targets)
            {
                _targetGroup.RemoveMember(target.target);
            }
        }
    }
}
