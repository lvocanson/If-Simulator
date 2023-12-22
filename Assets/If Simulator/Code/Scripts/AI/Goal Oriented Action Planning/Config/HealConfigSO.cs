using UnityEngine;

namespace IfSimulator.GOAP.Config
{
    [CreateAssetMenu(menuName ="AI/Heal Config", fileName ="Heal Config", order = 2)]
    public class HealConfigSO : ScriptableObject
    {
        public float SensorHealRadius = 10f;
        public float HealRadius = 3f;
        [Range(0, 100)] public float HealThreshold = 70;
        public int HealCost = 10;
        public float HealDelay = 4f;
        public int HealAmount = 30;

        public LayerMask HealableLayerMask;    
    }
}
