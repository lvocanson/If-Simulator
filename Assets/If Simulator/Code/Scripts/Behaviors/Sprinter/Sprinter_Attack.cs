using System.Collections;
using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;

public class Sprinter_Attack : BaseState
{
    [ShowNonSerializedField, Tooltip("The target to move towards")] private Transform _target;
    [Header("References")]
    [SerializeField] private Enemy _enemy;
    
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
        if (obj.CompareTag("Player"))
            Manager.ChangeState(_chaseState);
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        
        //TODO ATTACK PLAYER
    }


    private void OnDisable()
    {
        _attackEvent.OnExit -= ExitAttackRange;
        _enemy.Agent.isStopped = false;
        
        if (_enemy.gameObject != null)
            return; // If the enemy is dead, don't do anything
        
        if (_attackSprinter != null)
        {
            StopCoroutine(_attackSprinter);
            _attackSprinter = null;
        }
    }
}
