using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IfSimulator.GOAP.Config
{
    [CreateAssetMenu(menuName = "AI/WanderConfig", fileName = "WanderConfig", order = 2)]
    public class WanderConfigSO : ScriptableObject
    {
        public Vector2 WaitRangeBetweenWanders = new Vector2(1, 5);
        public float WanderRadius = 5f;
    }
}
