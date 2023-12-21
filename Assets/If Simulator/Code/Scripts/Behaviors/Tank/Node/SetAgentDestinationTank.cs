using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class SetAgentDestinationTank : ActionNodeSo
{
    private NavMeshAgent _agent;

    public override NodeSo DeepInitialize(Blackboard blackboard)
    {
        var clone = (SetAgentDestinationTank)base.DeepInitialize(blackboard);
        blackboard.Read("NavMeshAgent", out clone._agent);
        return clone;
    }

    protected override void OnUpdate()
    {
        Blackboard.Read("Destination", out Vector2 destination);
        _agent.SetDestination(destination);
        State = NodeState.Success;
    }
}
