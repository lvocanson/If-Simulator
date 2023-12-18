using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;

public class Kamikaz_Patrol : BaseState
{
    [SerializeField] private Enemy _enemy;

    [Header("State Machine")]
    [SerializeField] private Kamikaz_Chase _chase;
    [SerializeField] private Transform[] _waypoints;
    
    [Header("Data")]
    [SerializeField] private float _speed = 1f;
    
    [Header("Debug Waypoint")]
    [ShowNonSerializedField] private int _index = 0;
    
    [Header("Event")]
    [SerializeField] private PhysicsEvents _chaseColEvent;
    
    private void OnEnable()
    {
        _chaseColEvent.OnEnter += EnterOnChaseRange;
        _enemy.Agent.SetDestination(_waypoints[_index].position);
        _enemy.Agent.speed = _speed;
    }

    private void EnterOnChaseRange(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            _chase.SetTarget(obj.transform);
            Manager.ChangeState(_chase);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Changement de waypoint
        if(Vector3.Distance(transform.position, _waypoints[_index].position) < .5f)
        {
            _index++;
            if (_index >= _waypoints.Length)
                _index = 0;
            
            _enemy.Agent.SetDestination(_waypoints[_index].position);
        }
    }
    private void OnDisable()
    {
        _chaseColEvent.OnEnter -= EnterOnChaseRange;
    }
}
