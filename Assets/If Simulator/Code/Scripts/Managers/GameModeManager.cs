using System.Collections;
using GameMode;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameModeManager : MonoBehaviour
    {
        [Header("Base")]
        [Scene]
        [SerializeField] private string _initialScene;

        [Header("Game Modes")]
        [SerializeField] private GameplayMode _gameplayMode;
        [SerializeField] private MainMenuMode _mainMenuMode;

        private IGameMode _currentMode;

        private bool _isSwitching;

        private void Awake()
        {
#if UNITY_EDITOR
            if (!SceneManager.GetSceneByName(_initialScene).IsValid()) SceneManager.LoadScene(_initialScene, LoadSceneMode.Additive);

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0:
                    HandleStart();
                    break;
                case 1:
                    _currentMode = _mainMenuMode;
                    StartCoroutine(_currentMode.OnStartEditor());
                    break;
                default:
                    _currentMode = _gameplayMode;
                    StartCoroutine(_currentMode.OnStartEditor());
                    break;
            }
#else
            HandleStart();
#endif
        }

        private void HandleStart()
        {
            StartCoroutine(SwitchMode(_mainMenuMode));
        }

        private IEnumerator SwitchMode(IGameMode mode, string mainScene = null)
        {
            if (_isSwitching) yield break;
            _isSwitching = true;

            // Fade in
            if (_currentMode != null)
            {
                App.InputManager.Lock();
                yield return App.Instance.Backdrop.Activate();
                yield return _currentMode.OnEnd();
            }
            else
            {
                App.Instance.Backdrop.SetActivePanel();
            }

            // Switch mode
            _currentMode = mode;
            yield return _currentMode.OnStart(mainScene);

            // Fade out
            yield return App.Instance.Backdrop.Release();
            App.InputManager.Unlock();

            // Start context
            _currentMode.StartContext();

            _isSwitching = false;
        }

        private IEnumerator SwitchMainScene(string newScene)
        {
            if (_currentMode == null) yield break;
            if (_isSwitching) yield break;

            _isSwitching = true;

            App.InputManager.Lock();
            yield return App.Instance.Backdrop.Activate();

            yield return _currentMode.OnSwitchMainScene(newScene);

            yield return App.Instance.Backdrop.Release();
            App.InputManager.Unlock();

            _currentMode.StartContext();

            _isSwitching = false;
        }

        public void SwitchScene(string newScene)
        {
            StartCoroutine(SwitchMainScene(newScene));
        }

        public void SwitchToMainMenu(string mainScene = null)
        {
            StartCoroutine(SwitchMode(_mainMenuMode, mainScene));
        }

        public void SwitchToGameplay(string mainScene = null)
        {
            StartCoroutine(SwitchMode(_gameplayMode, mainScene));
        }

        public void ReloadCurrentMode()
        {
            StartCoroutine(ReloadMode());
        }

        public void RetryCurrentMode()
        {
            StartCoroutine(ReloadMode(true));
        }

        private IEnumerator ReloadMode(bool isRetry = false)
        {
            if (_isSwitching) yield break;

            _isSwitching = true;

            App.InputManager.Lock();
            var activateBackdrop = _currentMode != null;

            if (activateBackdrop)
                yield return App.Instance.Backdrop.Activate();
            else
                App.Instance.Backdrop.SetActivePanel();

            if (isRetry)
                yield return _currentMode.OnRetry();
            else
                yield return _currentMode.OnRestart();

            yield return App.Instance.Backdrop.Release();
            App.InputManager.Unlock();

            _currentMode.StartContext();

            _isSwitching = false;
        }
    }
}
