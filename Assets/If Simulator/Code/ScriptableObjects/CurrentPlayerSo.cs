using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentPlayerData", menuName = "Player")]
public class CurrentPlayerSo : ScriptableObject
{
    public Player Player { get; private set; }

    public event Action OnPlayerLoaded;

    public void Load(Player player)
    {
        Player = player;
        OnPlayerLoaded?.Invoke();
    }
}
