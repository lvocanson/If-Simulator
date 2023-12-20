using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class SetAgentDestination : ActionNodeSo
{
    NavMeshAgent _agent;

    public override NodeSo DeepInitialize(Blackboard blackboard)
    {
        var clone = base.DeepInitialize(blackboard) as SetAgentDestination;
        blackboard.Read("GameObject", out GameObject go);
        clone._agent = go.GetComponent<NavMeshAgent>();
        return clone;
    }

    protected override void OnUpdate()
    {
        Blackboard.Read("Destination", out Vector3 destination);
        _agent.SetDestination(destination);
    }
}
