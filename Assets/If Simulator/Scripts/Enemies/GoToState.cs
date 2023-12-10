using UnityEngine;
using FiniteStateMachine;

public class GoToState : BaseState
{
    [SerializeField, Tooltip("The target to move towards.")]
    private Transform _target;
    [SerializeField, Tooltip("The speed at which to move towards the target.")]
    private float _speed = 1f;

    [SerializeField] private BaseState _nextState;

    private void OnEnable()
    {
        Debug.Log("GoTo" + _target.name + ": OnEnable");
    }

    private void Update()
    {
        // Move towards the target.
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target.position) < 0.01f)
        {
            // We've reached the target, so change to the next state.
            Manager.ChangeState(_nextState);
        }
    }

    private void OnDisable()
    {
        Debug.Log("GoTo" + _target.name + ": OnDisable");
    }
}
