using System.Collections.Generic;
using UnityEngine;

public class Action_Wander : Action_Base
{
    [SerializeField] float SearchRange = 10f;
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Wander) });
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
        Vector3 loc;

    }

    public override void OnDesactivated()
    {
    }

    public override void OnTick()
    {
    }
}
