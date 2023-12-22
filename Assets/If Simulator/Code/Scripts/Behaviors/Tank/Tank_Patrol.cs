using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;

public class Tank_Patrol : BaseState
{
    [SerializeField] private Enemy _enemy;

    [Header("State Machine")]
    [SerializeField] private Tank_Chase _chase;
    [SerializeField] private Transform[] _waypoints;

    [Header("Data")]
    [SerializeField] private float _speed = 1f;

    [Header("Debug")]
    [ShowNonSerializedField] private int _index = 0;

    [Header("Event")]
    [SerializeField] private PhysicsEvents _chaseColEvent;


    private void OnEnable()
    {
        _chaseColEvent.OnEnter += EnterOnChaseRange;
        _enemy.Agent.SetDestination(_waypoints[_index].position);
        _enemy.Agent.speed = _speed;
    }

    // Enemy trigger is on Trigger Enemy layer, which will only collide with Trigger Player layer (bound to the Trigger GameObject in the Player)
    private void EnterOnChaseRange(Collider2D obj)
    {
        if (!obj.CompareTag("Player") || !obj.GetComponent<Player>()) return;

        _chase.SetTarget(obj.transform);
        Manager.ChangeState(_chase);
    }

    private void Update()
    {
        if (_waypoints.Length > 0 && Vector3.Distance(transform.position, _waypoints[_index].position) < .5f)
        {
            _index = (_index + 1) % _waypoints.Length;
            _enemy.Agent.SetDestination(_waypoints[_index].position);
            transform.up = _enemy.Agent.velocity.normalized;
        }
    }

    private void OnDisable()
    {
        _chaseColEvent.OnEnter -= EnterOnChaseRange;
    }
}
