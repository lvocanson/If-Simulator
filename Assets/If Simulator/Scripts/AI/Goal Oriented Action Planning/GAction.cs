using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SAP2D;

namespace GOAP
{
    public abstract class GAction : MonoBehaviour
    {
        public string actionName = "Action";
        public float cost = 1.0f;
        public GameObject target;
        public string targetTag;
        public float duration = 0;
        public WorldState[] preConditions;
        public WorldState[] afterEffects;
        public SAP2DAgent agent;

        public Dictionary<string, int> preconditions;
        public Dictionary<string, int> effects;

        public WorldStates agentBeliefs;

        public bool running = false;

        public GAction()
        {
            preconditions = new Dictionary<string, int>();
            effects = new Dictionary<string, int>();
        }

        public void Awake()
        {
            agent = gameObject.GetComponent<SAP2DAgent>();

            if (preConditions != null)
            {
                foreach (WorldState w in preConditions)
                {
                    preconditions.Add(w.key, w.value);
                }
            }

            if (afterEffects != null)
            {
                foreach (WorldState w in afterEffects)
                {
                    effects.Add(w.key, w.value);
                }
            }
        }

        public bool IsAchievable()
        {
            return true;
        }

        public bool IsAchievableGiven(Dictionary<string, int> conditions)
        {
            foreach (KeyValuePair<string, int> p in preconditions)
            {
                if (!conditions.ContainsKey(p.Key))
                {
                    return false;
                }
            }
            return true;
        }

        public abstract bool PrePerform();
        public abstract bool PostPerform();
    }
}
