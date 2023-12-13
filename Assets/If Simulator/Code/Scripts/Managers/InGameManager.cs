using GameMode;
using UnityEngine;

namespace Managers
{
    public abstract class InGameManager : MonoBehaviour
    {
        protected virtual void Awake()
        {
            LevelContext.Instance.OnInitialized += OnContextInitialized;
            LevelContext.Instance.OnStarted     += OnContextStarted;
            LevelContext.Instance.OnQuit        += OnContextQuit;
        }

        protected virtual void OnDestroy()
        {
            if (!LevelContext.Instance) return;
            LevelContext.Instance.OnInitialized -= OnContextInitialized;
            LevelContext.Instance.OnStarted     -= OnContextStarted;
            LevelContext.Instance.OnQuit        -= OnContextQuit;
        }

        protected abstract void OnContextInitialized(GameModeStartMode mode);
        protected abstract void OnContextStarted(GameModeStartMode mode);
        protected abstract void OnContextQuit(GameModeQuitMode mode);
    }
}