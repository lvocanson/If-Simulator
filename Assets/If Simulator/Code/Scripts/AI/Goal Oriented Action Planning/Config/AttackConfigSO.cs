using UnityEngine;

namespace IfSimulator.GOAP.Config
{
    [CreateAssetMenu(menuName ="AI/Attack Config", fileName ="Attack Config", order = 1)]
    public class AttackConfigSO : ScriptableObject
    {
        public float SensorRadius = 10f;
        public float MeleeAttackRadius = 7f;
        public int MeleeAttackCost = 5;
        public float AttackDelay = 0.5f;
        public float AttackDamage = 20f;

        public LayerMask AttackableLayerMask;
        public GameObject ProjectilePrefab;
    }

}

