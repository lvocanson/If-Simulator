using UnityEngine;

public class InitLookAt2DWithPlayer : MonoBehaviour
{
    [SerializeField] private LookAt2D _lookAt2D;
    [SerializeField] private CurrentPlayerSo _playerSo;

    private void Awake()
    {
        _playerSo.OnPlayerLoaded += OnPlayerLoaded;
    }

    private void OnPlayerLoaded()
    {
        _lookAt2D.Target = _playerSo.Player.transform;
    }

    private void OnDestroy()
    {
        _playerSo.OnPlayerLoaded -= OnPlayerLoaded;
    }
}
