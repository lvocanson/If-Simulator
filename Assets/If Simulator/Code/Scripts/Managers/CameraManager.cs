using Cinemachine;
using GameMode;
using Managers;
using UnityEngine;

public class CameraManager : InGameManager
{
    [SerializeField] private CinemachineVirtualCamera _currentCamera;
    [SerializeField] private CinemachineTargetGroup _targetGroup;
    
    [SerializeField] private CurrentPlayerSo _currentPlayerSo;

    private Camera _mainCamera;
    public Camera MainCamera => _mainCamera;
    

    protected override void OnContextInitialized(GameModeStartMode mode)
    {
        _mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    protected override void OnContextStarted(GameModeStartMode mode)
    {
        _currentPlayerSo.OnPlayerLoaded += AddPlayerAsTarget;
    }

    protected override void OnContextQuit(GameModeQuitMode mode)
    {
        _currentPlayerSo.OnPlayerLoaded -= AddPlayerAsTarget;
    }
    
    private void AddPlayerAsTarget()
    {
        AddTarget(_currentPlayerSo.Player.transform, 1, 7);
        AddTarget(_currentPlayerSo.Player.PlayerAim.AimCursor.transform, 0.6f, 1);
    }
    
    public void AddTarget(Transform target, float weight, float radius)
    {
        _targetGroup.AddMember(target, weight, radius);
    }
    
    public void RemoveTarget(Transform target)
    {
        _targetGroup.RemoveMember(target);
    }
}
