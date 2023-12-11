using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine; 

public class Sprinter_Chase : BaseState
{
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    [SerializeField] private BaseState _nextState;
    [SerializeField] private float _speed = 1f;

    private void OnEnable()
    {
        Debug.Log("Focus : " + _target.name + " OnEnabled.");
        _target = (Transform)Args[0]; 
    }
    void Update()
    {
        //S'il n'est plus dans la range de chase
        //Manager.ChangeState(_nextState);

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        // S'il est dans la range d'attaque alors : 
        if (Vector3.Distance(transform.position, _target.position) < 0.1f)
        {
            Manager.ChangeState(_nextState);
        }
    }

    private void OnDisable()
    {
        Debug.Log("Focus : " + _target.name + " OnDisabled.");
    }
}
