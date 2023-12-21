using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class Dash : ActionNodeSo
{
    private NavMeshAgent _agent;

    protected override void OnUpdate()
    {
        Blackboard.Read("Dash", out bool dash);
        Vector2 RandomPos = new(Random.Range(-10, 10), Random.Range(-10, 10));
        if (dash)
        {
            _agent.SetDestination(RandomPos);
            State = NodeState.Success;
        }
        else
        {
            State = NodeState.Failure;
        }
    }
}
