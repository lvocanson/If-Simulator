using UnityEngine;
using BehaviorTree;

public class CheckPlayerInAttackRange : ActionNodeSo
{
    private static int _playerLayerMask = 1 << 6;

    private Transform _transform;

    public CheckPlayerInAttackRange(Transform transform)
    {
        _transform = transform;
    }

    protected override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
