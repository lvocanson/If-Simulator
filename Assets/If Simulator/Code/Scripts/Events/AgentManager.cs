using UnityEngine;
using UnityEngine.AI;

public class AgentManager : MonoBehaviour, IVector3EventListener
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Vector3Event _destinationEvent;

#if UNITY_EDITOR
    private void Awake()
    {
        if (_agent == null || _destinationEvent == null)
            Debug.LogError("AgentManager: Missing references!", this);
    }
#endif
    private void OnEnable()
    {
        _destinationEvent.Register(this);
    }

    public void OnEventRaised(Vector3 value)
    {
        _agent.SetDestination(value);
    }

    private void OnDisable()
    {
        _destinationEvent.Unregister(this);
    }
}
