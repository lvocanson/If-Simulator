using UnityEngine;

public class GOAPPlanner : MonoBehaviour
{
    Goal_Base[] Goals;
    Action_Base[] Actions;

    Goal_Base ActiveGoal;
    Action_Base ActiveAction;
    private void Awake()
    {
        Goals = GetComponents<Goal_Base>();
        Actions = GetComponents<Action_Base>();
    }
    void Update()
    {
        foreach(var goal in Goals) 
            goal.OnTickGoal();

        Goal_Base bestGoal = null;
        Action_Base bestAction = null;

        foreach(var goal in Goals)
        {
            goal.OnTickGoal();

            if (!goal.CanRun())
                continue;

            if (!(bestGoal == null || goal.CalculatePriority() > bestGoal.CalculatePriority()))
                continue;

            Action_Base candidateAction = null;
            foreach(var action in Actions)
            {
                if (action.GetSupportedGoals().Contains(goal.GetType()))
                    continue;

                if (candidateAction == null || action.GetCost() < candidateAction.GetCost())
                    candidateAction = action;
            }

            if (candidateAction != null)
            {
                bestGoal = goal;
                bestAction = candidateAction;
            }
        }

        if (ActiveGoal == null && bestGoal != null)
        {
            ActiveGoal = bestGoal;
            ActiveAction = bestAction;
        }
        else if (ActiveGoal == bestGoal)
        {
            if (ActiveAction != bestAction)
            {
                ActiveAction.OnDesactivated();

                ActiveAction = bestAction;

                ActiveAction.OnActivated();
            }

        }
        else if (ActiveGoal != bestGoal)
        {
            ActiveGoal.OnGoalDeactivated();
            ActiveAction.OnDesactivated();

            ActiveGoal = bestGoal;
            ActiveAction = bestAction;

            if (ActiveGoal != null)
                ActiveGoal.OnGoalActivated(ActiveAction);
            if (ActiveAction != null)
                ActiveAction.OnActivated();
        }

        if (ActiveAction != null)
        {
            ActiveAction.OnTick();
        }
    }
}
