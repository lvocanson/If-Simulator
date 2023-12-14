using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using NaughtyAttributes;
using SAP2D;
using UnityEngine.Serialization;

public class Sprinter_Patrol : BaseState
{
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    [SerializeField] private BaseState _chase;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _playerRange = 2f;
    [ShowNonSerializedField] private int _index = 0;
    [SerializeField] private SAP2DAgent _SAPAgent;
    
    
    private void OnEnable()
    {
        _SAPAgent.Target = _waypoints[_index];
    }

    // Update is called once per frame
    void Update()
    {
        //Si le joueur est trop proche
        if (Vector3.Distance(transform.position, _target.position) < _playerRange)
        {
            Manager.ChangeState(_chase);
        }
        
        //Changement de waypoint
        else if(Vector3.Distance(transform.position, _waypoints[_index].position) < .5f)
        {
            _index++;
            if (_index >= _waypoints.Length)
                _index = 0;
            
            _SAPAgent.Target = _waypoints[_index];
        }
        
    }
    private void OnDisable()
    {
        
    }
}
