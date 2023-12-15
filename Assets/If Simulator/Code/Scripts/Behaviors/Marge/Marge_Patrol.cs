using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;
using SAP2D;
using UnityEngine.Serialization;

public class Marge_Patrol : BaseState
{
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    
    [Header("State Machine")]
    [SerializeField] private BaseState _chase;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private SAP2DAgent _SAPAgent;
    
    [Header("Data")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _playerRange = 2f;
    
    [Header("Debug Waypoint")]
    [ShowNonSerializedField] private int _index = 0;
    
    [Header("Event")]
    [SerializeField] private PhysicsEvents _chaseColEvent;
    
    private void OnEnable()
    {
        _chaseColEvent.OnEnter += EnterOnChaseRange;
        _SAPAgent.Target = _waypoints[_index];
        _SAPAgent.MovementSpeed = _speed;
    }

    private void EnterOnChaseRange(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
            Manager.ChangeState(_chase);
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
            
            _SAPAgent.Target = _waypoints[_index];
        }
    }
    private void OnDisable()
    {
        _chaseColEvent.OnEnter -= EnterOnChaseRange;
    }
}
