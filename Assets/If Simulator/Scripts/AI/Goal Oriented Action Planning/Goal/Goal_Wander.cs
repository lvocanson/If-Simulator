using UnityEngine;

public class Goal_Wander : Goal_Base
{
    [SerializeField] int WanderPriority = 20;

    [SerializeField] float PriorityBuildRate = 1f;
    [SerializeField] float PriorityDecaydRate = 1f;
    float CurrentPriority = 0;

    public override void OnTickGoal()
    {
        CurrentPriority += PriorityDecaydRate * Time.deltaTime;
        CurrentPriority += PriorityBuildRate * Time.deltaTime;
    }
    public override int CalculatePriority()
    {
        return Mathf.FloorToInt(CurrentPriority);
    }

    public override bool CanRun()
    {
        return true;
    }
}
