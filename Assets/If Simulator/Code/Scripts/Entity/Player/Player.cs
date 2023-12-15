using System;
using UnityEngine;

public class Player : DamageabaleEntity
{
    [Header("References")]
    [SerializeField] private PlayerAttackManager _playerAttackManager;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAim playerAim;
    

    public PlayerAttackManager PlayerAttackManager => _playerAttackManager;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerAim PlayerAim => playerAim;
}
