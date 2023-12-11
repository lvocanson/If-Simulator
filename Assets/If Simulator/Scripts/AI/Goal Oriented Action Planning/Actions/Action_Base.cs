using System.Collections.Generic;
using UnityEngine;

public class Action_Base : MonoBehaviour
{
    protected Goal_Base LinkedGoal;
    public virtual List<System.Type> GetSupportedGoals()
    {
        return null;
    }

    public virtual float GetCost()
    {
        return 0f;
    }

    public virtual void OnActivated()
    {

    }

    public virtual void OnDesactivated()
    {

    }

    public virtual void OnTick()
    {

    }
}
