using UnityEngine;
using UnityEngine.AI;

public class Enemy : DamageableEntity
{
    [Header("Enemy")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _lifeBarPrefab;
    [SerializeField] private Transform _lifeBarPosition;
    
    private InGameLifeBar _lifeBar;
    
    public NavMeshAgent Agent => _agent;


    private void OnEnable()
    {
        OnHealthChanged += _lifeBar.SetHealth;
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        
        OnHealthChanged -= _lifeBar.SetHealth;
    }

    protected override void Awake()
    {
        base.Awake();
        
        _lifeBar = Instantiate(_lifeBarPrefab, transform.position, Quaternion.identity).GetComponent<InGameLifeBar>();
        _lifeBar.Initialize(_lifeBarPosition, this);
        _lifeBar.gameObject.name = $"{gameObject.name} LifeBar";
    }

    protected override void Start()
    {
        base.Start();
        
        _agent.baseOffset = -0.005293157f;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    protected override void Die()
    {
        base.Die();
        
        Destroy(gameObject);
        Destroy(_lifeBar.gameObject);
    }
}
