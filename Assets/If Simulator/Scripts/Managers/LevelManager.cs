using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level _currentLevel;

    private void Start()
    {
        _currentLevel.Initialize();
    }
}
