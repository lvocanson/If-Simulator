using BehaviorTree;
using UnityEngine;

public class Cooldown : ActionNodeSo
{
    [SerializeField] private float _cooldown;
    [SerializeField] private bool _onCooldownAtStart = false;
    private float _lastTime = -1000;

    public override NodeSo DeepInitialize(Blackboard blackboard)
    {
        var clone = (Cooldown)base.DeepInitialize(blackboard);
        if (_onCooldownAtStart)
        {
            clone._lastTime = Time.time;
        }
        return clone;
    }

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
