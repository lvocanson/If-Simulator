using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class Helper : GAgent
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            SubGoal s1 = new SubGoal("isIdle", 1, true);
            goals.Add(s1, 3);
            SubGoal s2 = new SubGoal("isSafe", 1, true);
            goals.Add(s2, 2);
            SubGoal s3 = new SubGoal("isHungry", 1, true);
            goals.Add(s3, 1);
        }
    }
}
