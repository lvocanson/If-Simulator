using UnityEngine;

namespace IfSimulator.GOAP.Config
{
    [CreateAssetMenu(menuName ="AI/Heal Config", fileName ="Heal Config", order = 1)]
    public class HealConfigSO : ScriptableObject
    {
        public float SensorHealRadius = 10f;
        public float HealRadius = 2f;
        public int HealCost = 1;
        public float HealDelay = 1f;

        public LayerMask HealableLayerMask;    
    }
}
