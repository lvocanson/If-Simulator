using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;

public class Sprinter_Patrol : BaseState
{
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    [SerializeField] private BaseState _chase;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _range = 2f;
    private int _index = 0;
    
    
    
    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_index].position, _speed * Time.deltaTime);
        //Changement de waypoint
        if(Vector3.Distance(transform.position, _waypoints[_index].position) < 0.1f)
        {
            _index++;
            if (_index >= _waypoints.Length)
                _index = 0;
        }
        //Si le joueur est trop proche
        if (Vector3.Distance(transform.position, _target.position) < _range)
        {
            Manager.ChangeState(_chase);
        }
    }
    private void OnDisable()
    {
        
    }
}
