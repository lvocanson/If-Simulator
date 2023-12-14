using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsEvents : MonoBehaviour
{
    public event Action<Collider2D> OnEnter;  
    public event Action<Collider2D> OnExit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnEnter?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnExit?.Invoke(other);
    }
}
