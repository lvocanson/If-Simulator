using UnityEngine;
using FiniteStateMachine;

public class Kamikaz_WaitPlayer : BaseState
{
    [SerializeField] private Enemy _enemy;
    
    [Header("State Machine")]
    [SerializeField] private Kamikaz_Chase _chase;
    
    [Header("Event")]
    [SerializeField] private PhysicsEvents _chaseColEvent;
    
    
    private void OnEnable()
    {
        _chaseColEvent.OnEnter += EnterOnChaseRange;
    }

    private void EnterOnChaseRange(Collider2D obj)
    {
        if (obj.CompareTag("Player") && obj.GetComponent<Player>())
        {
            _chase.SetTarget(obj.transform);
            Manager.ChangeState(_chase);
        }
    }
    
    private void OnDisable()
    {
        _chaseColEvent.OnEnter -= EnterOnChaseRange;
    }
}
