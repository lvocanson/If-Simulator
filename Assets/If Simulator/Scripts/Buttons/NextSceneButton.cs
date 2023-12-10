using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButton : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
