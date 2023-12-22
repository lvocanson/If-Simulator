using System;
using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class ExitLevel : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        _sceneLoader = GetComponent<SceneLoader>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _sceneLoader.SwitchScene();
        }
    }
}
