using System.Collections.Generic;
using UnityEngine;

public class Action_Chase : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Chase) });

    Goal_Chase ChaseGoal;
    public override List<System.Type> GetSupportedGoals()
    {
        return null;
    }

    public override float GetCost()
    {
        return 0f;
    }
    public override void OnActivated()
    {
        ChaseGoal = (Goal_Chase)LinkedGoal;
    }
    public override void OnDesactivated()
    {
        ChaseGoal = null;
    }
    public override void OnTick()
    {
    }
}
