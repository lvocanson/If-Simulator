using System.Collections;

namespace GameMode
{
    public interface IGameMode
    {
        public IEnumerator OnStart(string mainScene = null);
        public IEnumerator OnStartEditor();
        public IEnumerator OnSwitchMainScene(string newScene);
        public IEnumerator OnRestart();
        public IEnumerator OnRetry();
        public IEnumerator OnEnd();

        public void StartContext();
    }

    public enum GameModeState
    {
        Starting,
        Started,
        Loading,
        Ending,
        Ended
    }

    public enum GameModeStartMode
    {
        Start,
        Restart,
        Retry
    }

    public enum GameModeQuitMode
    {
        Restart,
        Retry,
        Quit
    }
}
