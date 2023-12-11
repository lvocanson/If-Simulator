using UnityEngine;

public class Goal_Chase : Goal_Base
{
    [SerializeField] int PriorityChase = 60;
    [SerializeField] float MinAwarenessToChase = 1.5f; 
    [SerializeField] float AwarenessToStopChase = 1f;

    public override void OnTickGoal()
    {
        
    }

    public override void OnGoalActivated(Action_Base _linkedAction)
    {
        base.OnGoalActivated(_linkedAction);
    }

    public override void OnGoalDeactivated()
    {
    }

    public override int CalculatePriority()
    {
        return 0;
    }

    public override bool CanRun()
    {
        return true;
    }
}
