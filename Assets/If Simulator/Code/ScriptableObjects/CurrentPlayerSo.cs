using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentPlayerData", menuName = "Player")]
public class CurrentPlayerSo : ScriptableObject
{
    public Player Player { get; private set; }

    public event Action OnPlayerLoaded;
    public event Action OnPlayerStarted;

    public void Load(Player player)
    {
        Player = player;
        OnPlayerLoaded?.Invoke();
    }
    
    public void Start()
    {
        OnPlayerStarted?.Invoke();
    }
}
