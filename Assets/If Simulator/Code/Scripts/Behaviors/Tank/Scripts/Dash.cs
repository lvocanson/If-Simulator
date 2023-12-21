using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class Dash : ActionNodeSo
{
    private NavMeshAgent _agent;
    private Vector3 _position;
    [SerializeField] private float _speed = 0f;
    private float _agentSpeed = 0f;

    public override NodeSo DeepInitialize(Blackboard blackboard)
    {
        var clone = (Dash)base.DeepInitialize(blackboard);
        blackboard.Read("Agent", out clone._agent);
        return clone;
    }

    protected override void OnEnter()
    {
        _agentSpeed = _agent.speed;
        _agent.speed = _speed;

        _position = new(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        _agent.SetDestination(_position);
    }
    protected override void OnUpdate()
    {
        var distance = Vector3.Distance(_agent.transform.position, _position);
        if (distance < 0.01f)
        {
            State = NodeState.Success;
        }
    }

    protected override void OnExit()
    {
        _agent.speed = _agentSpeed;
    }
}
