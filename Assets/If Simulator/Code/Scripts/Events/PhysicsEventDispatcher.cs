using UnityEngine;
using UnityEngine.Events;

public class PhysicsEventDispatcher : MonoBehaviour
{
    [SerializeField] private PhysicsEvents _physicsEvents;
    [SerializeField] private UnityEvent _onTriggerEnter;
    [SerializeField] private UnityEvent _onTriggerExit;
    [SerializeField] private UnityEvent _onTriggerStay;

    private void OnEnable()
    {
        _physicsEvents.OnEnter += OnEnter;
        _physicsEvents.OnExit += OnExit;
        _physicsEvents.OnStay += OnStay;
    }

    private void OnEnter(Collider2D _)
    {
        _onTriggerEnter.Invoke();
    }

    private void OnExit(Collider2D _)
    {
        _onTriggerExit.Invoke();
    }

    private void OnStay(Collider2D _)
    {
        _onTriggerStay.Invoke();
    }

    private void OnDisable()
    {
        _physicsEvents.OnEnter -= OnEnter;
        _physicsEvents.OnExit -= OnExit;
        _physicsEvents.OnStay -= OnStay;
    }
}
