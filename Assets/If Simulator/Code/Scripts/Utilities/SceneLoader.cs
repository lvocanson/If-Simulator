using NaughtyAttributes;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField, Scene] private string _sceneToLoad;

    public void SwitchScene()
    {
        App.Instance.GameMode.SwitchScene(_sceneToLoad);
    }

    public void LoadMainMenuScene()
    {
        App.Instance.GameMode.SwitchToMainMenu();
    }

    public void LoadGameplayScene()
    {
        App.Instance.GameMode.SwitchToGameplay();
    }
}
