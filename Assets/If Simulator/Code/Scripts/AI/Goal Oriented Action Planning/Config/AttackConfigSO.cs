using UnityEngine;

namespace IfSimulator.GOAP.Config
{
    [CreateAssetMenu(menuName ="AI/Attack Config", fileName ="Attack Config", order = 1)]
    public class AttackConfigSO : ScriptableObject
    {
        public float SensorRadius = 10f;
        public float MeleeAttackRadius = 2f;
        public int MeleeAttackCost = 1;
        public float AttackDelay = 1f;

        public LayerMask AttackableLayerMask;
    }

}

