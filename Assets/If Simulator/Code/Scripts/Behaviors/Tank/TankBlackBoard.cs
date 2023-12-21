using System;
using BehaviorTree;
using NaughtyAttributes;
using UnityEngine;

public class TankBlackBoard : MonoBehaviour
{
    [SerializeField] private LookAt2D _lookAt2D;
    [SerializeField] private BTreeRunner _runner;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform[] _waypoints;

    [SerializeField] private PhysicsEvents _chaseColEvent;
    [SerializeField] private PhysicsEvents _attackColEvent;
    //[SerializeField] private Shooter _shooter;

    [Header("Debug")]
    [ShowNonSerializedField] private int _index = 0;

    private void OnEnable()
    {
        _chaseColEvent.OnEnter += OnPlayerEnteredRange;
        _chaseColEvent.OnExit += OnPlayerExitedRange;
    }

    private void OnPlayerEnteredRange(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            _runner.Blackboard.Write("isPlayerInRange", true);
            _lookAt2D.Target = collider2D.transform.parent;
        }
    }

    private void OnPlayerExitedRange(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            _runner.Blackboard.Write("isPlayerInRange", false);
            _lookAt2D.Target = null;
        }
    }

    private void OnDisable()
    {
        _chaseColEvent.OnEnter -= OnPlayerEnteredRange;
        _chaseColEvent.OnExit -= OnPlayerExitedRange;
    }
}
