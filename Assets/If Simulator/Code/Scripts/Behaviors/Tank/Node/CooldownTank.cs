using BehaviorTree;
using UnityEngine;

public class CooldownTank : ActionNodeSo
{
    [SerializeField] private float _cooldown;
    private float _lastTime = -1000;

    protected override void OnUpdate()
    {
        if (Time.time - _lastTime > _cooldown)
        {
            State = NodeState.Success;
            _lastTime = Time.time;
        }
        else
        {
            State = NodeState.Failure;
        }
    }
}
