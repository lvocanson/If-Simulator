using System.Collections;
using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;

public class Sprinter_Attack : BaseState
{
    [ShowNonSerializedField, Tooltip("The target to move towards")] private Transform _target;
    [Header("References")]
    [SerializeField] private Enemy _enemy;
    
    public void SetTarget(Transform target) => _target = target;
    private Coroutine _attackSprinter;


    private void OnEnable()
    {
        _enemy.Agent.isStopped = true;

        _attackSprinter ??= StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        
        //TODO ATTACK PLAYER
    }


    private void OnDisable()
    {
        if (_enemy.gameObject != null)
            return; // If the enemy is dead, don't do anything
        
        if (_attackSprinter != null)
        {
            StopCoroutine(_attackSprinter);
            _attackSprinter = null;
        }
        _enemy.Agent.isStopped = false;
    }
}
