using Ability;
using UnityEngine;

public class Player : DamageableEntity
{
    [Header("References")]
    [SerializeField] private PlayerAttackManager _playerAttackManager;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private CurrentPlayerSo _data;
    
    [Header("For Debug")]
    [SerializeField] private SoAbilityBase _firstSpell;
    [SerializeField] private SoAbilityBase _secondSpell;
    
    public PlayerAttackManager PlayerAttackManager => _playerAttackManager;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerAim PlayerAim => playerAim;

    protected override void Awake()
    {
        base.Awake();

        _data.Load(this);
    }

    protected override void Start()
    {
        base.Start();
        
        _playerAttackManager.ChangeFirstSpell(_firstSpell);
        _playerAttackManager.ChangeSecondSpell(_secondSpell);
        
        _data.Start();
    }
    
    protected override void Die()
    {
        base.Die();
        
        gameObject.SetActive(false);
        LevelContext.Instance.LevelManager.RestartLevel();
    }
}
