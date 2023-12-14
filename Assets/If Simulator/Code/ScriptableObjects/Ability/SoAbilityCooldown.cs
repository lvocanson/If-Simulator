using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ability
{
    [CreateAssetMenu(fileName = "NewActiveSo", menuName = "Scriptable Objects/Active Ability", order = 0)]
    public class SoAbilityCooldown : SoAbilityBase
    {
        public float Cooldown => _cooldown;
        public bool IsHoldable => _isHoldable;
        
        public AnimationCurve EvolutionCurve => _evolutionCurve;
        public float Delay => _delay;
        
        [Header("Ability cooldown")]
        [SerializeField] private float _cooldown;
        [SerializeField] private bool _isHoldable;
        
        [SerializeField, HideIf("IsHoldable")] private AnimationCurve _evolutionCurve;
        [SerializeField, ShowIf("IsHoldable")] private float _delay;
    }
    
    public static class AnimationCurveExtension
    {
        public static float Duration(this AnimationCurve @this)
        {
            return @this.keys[@this.length - 1].time;
        }
    }
}