using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;

public class Sprinter_Patrol : BaseState
{
    
    [SerializeField] private BaseState _nextState;
    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si le joueur est trop proche
        
        Manager.ChangeState(_nextState);
    }
    private void OnDisable()
    {
        
    }
}
