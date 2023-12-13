using Cinemachine;
using UnityEngine;

namespace Utility
{
    public class CameraSetup : MonoBehaviour
    {
        [SerializeField] private CurrentPlayerSo _currentPlayerSo;
    
        private CinemachineVirtualCamera _virtualCamera;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();

#if UNITY_EDITOR
            Player player = FindFirstObjectByType<Player>();
            if (player) _virtualCamera.Follow = player.transform;
#endif
        }

        private void OnEnable()
        {
            _currentPlayerSo.OnPlayerLoaded += UpdateTarget;
        }

        private void OnDisable()
        {
            _currentPlayerSo.OnPlayerLoaded -= UpdateTarget;
        }

        private void UpdateTarget()
        {
            _virtualCamera.Follow = _currentPlayerSo.Player.transform;
        }
    }

}
