using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class ExitLevel : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _sceneLoader.SwitchScene();
        }
    }
}
