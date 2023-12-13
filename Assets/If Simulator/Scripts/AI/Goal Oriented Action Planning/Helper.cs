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
            SubGoal s1 = new SubGoal("isSafe", 1, true);
            goals.Add(s1, 3);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
