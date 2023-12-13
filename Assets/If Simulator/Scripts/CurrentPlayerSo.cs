using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentPlayerData", menuName = "Player")]
public class CurrentPlayerSo : ScriptableObject
{
    public PlayerMovement Player { get; private set; }

    public event Action OnPlayerLoaded;

    public void Load(PlayerMovement player)
    {
        Player = player;
        Debug.Log(player);
        OnPlayerLoaded?.Invoke();
    }
}
