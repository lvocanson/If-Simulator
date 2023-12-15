using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMode
{
    [Serializable]
    public class GameplayMode : IGameMode
    {
        private GameModeState _state = GameModeState.Ended;

        [Header("Scenes")]
        [SerializeField, Scene] private string _mainScene;
        [SerializeField, Scene] private string _uiScene;
        
        [Header("Events")]
        [SerializeField] private EventSo _onMainSceneLoad;

        
        public IEnumerator OnStart(string mainScene = null)
        {
            if (_state != GameModeState.Ended) yield break;
            _state = GameModeState.Starting;

            if (mainScene != null)
                _mainScene = mainScene;
            yield return LoadAllSceneAsync();

            yield return OnLoad(GameModeStartMode.Start);

            _state = GameModeState.Started;
            App.InputManager.SwitchMode(InputManager.InputMode.Gameplay);
        }

        public IEnumerator OnStartEditor()
        {
            yield return null;
            _mainScene = SceneManager.GetActiveScene().name;

            yield return SceneManager.LoadSceneAsync(_uiScene, LoadSceneMode.Additive);

            yield return OnLoad(GameModeStartMode.Start);

            StartContext();

            _state = GameModeState.Started;
        }

        private IEnumerator OnLoad(GameModeStartMode startMode)
        {
            var scene = SceneManager.GetSceneByName(_mainScene);
            SceneManager.SetActiveScene(scene);

            _onMainSceneLoad.Invoke();
            if (LevelContext.Instance) LevelContext.Instance.InitializeContext(startMode);
            yield return null;
        }

        public IEnumerator OnSwitchMainScene(string newScene)
        {
            if (LevelContext.Instance) LevelContext.Instance.QuitContext(GameModeQuitMode.Quit);
            yield return SceneManager.UnloadSceneAsync(_mainScene);
            _mainScene = newScene;
            yield return SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);
            yield return OnLoad(GameModeStartMode.Start);
        }

        private IEnumerator LoadAllSceneAsync()
        {
            yield return SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_mainScene));

            yield return SceneManager.LoadSceneAsync(_uiScene, LoadSceneMode.Additive);
        }

        public IEnumerator OnRestart()
        {
            if (LevelContext.Instance) LevelContext.Instance.QuitContext(GameModeQuitMode.Restart);

            yield return SceneManager.UnloadSceneAsync(_mainScene);
            yield return SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);

            yield return OnLoad(GameModeStartMode.Restart);
        }
        
        public IEnumerator OnRetry()
        {
            if (LevelContext.Instance) LevelContext.Instance.QuitContext(GameModeQuitMode.Retry);

            yield return SceneManager.UnloadSceneAsync(_mainScene);
            yield return SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);

            yield return OnLoad(GameModeStartMode.Retry);
        }

        public IEnumerator OnEnd()
        {
            if (_state != GameModeState.Started) yield break;

            _state = GameModeState.Ending;

            if (LevelContext.Instance) LevelContext.Instance.QuitContext(GameModeQuitMode.Quit);
            
            yield return UnLoadAllSceneAsync();
            
            _state = GameModeState.Ended;
        }

        private IEnumerator UnLoadAllSceneAsync()
        {
            yield return SceneManager.UnloadSceneAsync(_mainScene);
            
            yield return SceneManager.UnloadSceneAsync(_uiScene);
        }

        public void StartContext()
        {
            if (LevelContext.Instance) LevelContext.Instance.StartContext();
        }
    }
}