using System;
using UnityEngine;

public class PlayerXp : MonoBehaviour
{
    private float _currentXp = 0;
    private int _currentLevel = 1;
    [SerializeField] private float _currentXpToNextLevel = 100;
        
    [SerializeField] private float _xpPerKill = 10;
        
    public event Action<float, float, int> OnXpChanged; // => float1: currentXp, float2: currentXpToNextLevel, float3: currentLevel
    public event Action<int> OnLevelUp; // => int: currentLevel
        
    private void Start()
    {
        OnXpChanged?.Invoke(_currentXp, _currentXpToNextLevel, _currentLevel);
    }
        
    public void AddXp()
    {
        _currentXp += _xpPerKill;
        if (_currentXp >= _currentXpToNextLevel)
        {
            LevelUp();
        }
        OnXpChanged?.Invoke(_currentXp, _currentXpToNextLevel, _currentLevel);
    }
        
    private void LevelUp()
    {
        _currentXp = 0;
        _currentLevel++;
        _currentXpToNextLevel *= 1.5f;
        OnLevelUp?.Invoke(_currentLevel);
    }
}