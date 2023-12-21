using System.Collections;
using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;

public class Sprinter_Attack : BaseState
{
    [ShowNonSerializedField, Tooltip("The target to move towards")] private Transform _target;
    [Header("References")]
    [SerializeField] private Enemy _enemy;
    [SerializeField] private CircleCollider2D _hitAttack; 
    
    [Header("State Machine")]
    [SerializeField] private Sprinter_Chase _chaseState;
    
    [Header("Event")]
    [SerializeField] private PhysicsEvents _attackEvent;
    
    public void SetTarget(Transform target) => _target = target;
    private Coroutine _attackSprinter;


    private void OnEnable()
    {
        _enemy.Agent.isStopped = true;
        _attackEvent.OnExit += ExitAttackRange;
        _attackSprinter ??= StartCoroutine(Attack());
    }
    
    private void ExitAttackRange(Collider2D obj)
    {
        Debug.Log("EXIT RANGE");
        StopCoroutine(_attackSprinter);
        _attackSprinter = null;
        Manager.ChangeState(_chaseState);
    }

    private IEnumerator Attack()
    {
        while (_target != null)
        {
            Debug.Log("Attack melee");
            yield return new WaitForSeconds(1f);
        }
    }


    private void OnDisable()
    {
        _attackEvent.OnExit -= ExitAttackRange;
        
        if (_enemy.gameObject != null)
            return; // If the enemy is dead, don't do anything
        
        _enemy.Agent.isStopped = false;
        _hitAttack.enabled = false;
        
        if (_attackSprinter != null)
        {
            StopCoroutine(_attackSprinter);
            _attackSprinter = null;
        }
    }
}
