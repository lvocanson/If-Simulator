using BehaviorTree;
using UnityEngine;

public class TankBrain : Enemy
{
    [SerializeField] private BTreeRunner _btRunner;

    [Header("Tank")]
    [SerializeField] private float _2ndPhaseHealthThreshold;
    private bool _is2ndPhase = false;

    protected void OnEnable()
    {
        OnHealthChanged += CheckPhase;
    }

    private void CheckPhase(float health, float maxHealth)
    {
        if (!_is2ndPhase && health <= _2ndPhaseHealthThreshold)
        {
            _is2ndPhase = true;
            _btRunner.Blackboard.Write("2nd Phase", true);
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnHealthChanged -= CheckPhase;
    }
}
