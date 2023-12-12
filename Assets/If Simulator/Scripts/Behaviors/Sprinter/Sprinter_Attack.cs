using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using UnityEngine.Serialization;

public class Sprinter_Attack : BaseState
{
    [SerializeField, Tooltip("The target to move towards")]
    private Transform _target;
    [SerializeField] private BaseState _previousState;

    private void OnEnable()
    {
        //throw new NotImplementedException();
    }
    void Update()
    {
        Debug.Log("Mob : " + gameObject.name + " is attacking.");
        // Si le joueur est trop loin
        if (Vector3.Distance(transform.position, _target.position) > 1f)
        {
            Manager.ChangeState(_previousState);
        }
    }

    private void OnDisable()
    {
        //throw new NotImplementedException();
    }
}
