using BehaviorTree;
using UnityEngine;

public class NebulaBrain : Enemy
{
    [SerializeField] private BTreeRunner _btRunner;

    [Header("Nebula")]
    [SerializeField, Range(0, 100)] private float _2ndPhaseHealthThreshold;
    private bool _is2ndPhase = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnHealthChanged += CheckPhase;
    }

    private void CheckPhase(float health, float maxHealth)
    {
        if (!_is2ndPhase && (health / maxHealth) * 100 <= _2ndPhaseHealthThreshold)
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
