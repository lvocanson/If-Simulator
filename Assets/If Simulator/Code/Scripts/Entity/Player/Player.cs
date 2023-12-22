using UnityEngine;

public class Player : DamageableEntity
{
    [Header("References")]
    [SerializeField] private PlayerAttackManager _playerAttackManager;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAim playerAim;
    [SerializeField] private PlayerXp _playerXp;
    [SerializeField] private CurrentPlayerSo _data;
    
    public PlayerAttackManager PlayerAttackManager => _playerAttackManager;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerAim PlayerAim => playerAim;
    public PlayerXp PlayerXp => _playerXp;

    protected override void Awake()
    {
        base.Awake();

        _data.Load(this);
    }

    protected override void Start()
    {
        base.Start();
        
        _playerAttackManager.ChangeFirstSpell(null);
        _playerAttackManager.ChangeSecondSpell(null);

        _data.Start();
    }
    
    protected override void Die()
    {
        base.Die();
        
        gameObject.SetActive(false);
        LevelContext.Instance.LevelManager.RestartLevel();
    }
}
