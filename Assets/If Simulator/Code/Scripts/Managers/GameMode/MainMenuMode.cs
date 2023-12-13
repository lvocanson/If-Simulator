using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMode
{
    [Serializable]
    public class MainMenuMode : IGameMode
    {
        private GameModeState _state = GameModeState.Ended;
        [Scene]
        [SerializeField] private string _mainScene;
        [Scene]
        [SerializeField] private List<string> otherScene;

        public IEnumerator OnStart(string mainScene = null)
        {
            if (_state != GameModeState.Ended) yield break;
            _state = GameModeState.Starting;

            if (mainScene != null)
                _mainScene = mainScene;
            yield return LoadAllSceneAsync();

            yield return OnLoad(GameModeStartMode.Start);

            _state = GameModeState.Started;
            App.InputManager.SwitchMode(InputManager.InputMode.UI);
        }

        public IEnumerator OnStartEditor()
        {
            yield return null;
            _mainScene = SceneManager.GetActiveScene().name;
            
            foreach (var scene in otherScene)
            {
                yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
            yield return new WaitForSeconds(0.1f);
            _state = GameModeState.Started;
            App.InputManager.SwitchMode(InputManager.InputMode.UI);
        }

        private IEnumerator OnLoad(GameModeStartMode startMode)
        {
            var scene = SceneManager.GetSceneByName(_mainScene);
            SceneManager.SetActiveScene(scene);

            yield return null;
        }

        public IEnumerator OnSwitchMainScene(string newScene)
        {
            yield return SceneManager.UnloadSceneAsync(_mainScene);
            _mainScene = newScene;
            yield return SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);
        }

        private IEnumerator LoadAllSceneAsync()
        {
            yield return SceneManager.LoadSceneAsync(_mainScene, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_mainScene));
            foreach (var scene in otherScene)
            {
                yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
        }

        public IEnumerator OnRetry()
        {
            throw new NotImplementedException();
        }

        public IEnumerator OnEnd()
        {
            if (_state != GameModeState.Started) yield break;
            _state = GameModeState.Ending;

            yield return UnLoadAllSceneAsync();
            
            _state = GameModeState.Ended;
        }

        private IEnumerator UnLoadAllSceneAsync()
        {
            yield return SceneManager.UnloadSceneAsync(_mainScene);
            foreach (var scene in otherScene)
            {
                yield return SceneManager.UnloadSceneAsync(scene);
            }
        }

        public IEnumerator OnRestart()
        {
            throw new NotImplementedException();
        }

        public void StartContext()
        {
            Time.timeScale = 1f;
            //throw new NotImplementedException();
        }
    }
}