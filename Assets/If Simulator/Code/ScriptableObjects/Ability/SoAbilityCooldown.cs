using NaughtyAttributes;
using UnityEngine;

namespace Ability
{
    [CreateAssetMenu(fileName = "NewActiveSo", menuName = "Scriptable Objects/Active Ability", order = 0)]
    public class SoAbilityCooldown : SoAbilityBase
    {
        public float Cooldown => _cooldown;
        public bool IsHoldable => _isHoldable;
        public float Delay => _delay;
        public float AbilityDuration => _abilityDuration;

        public override void LevelUp()
        {
            base.LevelUp();
            _cooldown -= _cooldownDecreasePerLevel;
            _abilityDuration += _abilityDurationIncreasePerLevel;
        }

        [Header("Ability cooldown")]
        [SerializeField] private float _cooldown;
        [SerializeField] private bool _isHoldable;
        [SerializeField, ShowIf("IsHoldable")] private float _delay;
        [SerializeField, HideIf("IsHoldable")] private float _abilityDuration;
        
        [Header("Level up properties (Ability cooldown)")]
        [SerializeField] private float _cooldownDecreasePerLevel;
        [SerializeField] private float _abilityDurationIncreasePerLevel;
    }
    
    public static class AnimationCurveExtension
    {
        public static float Duration(this AnimationCurve @this)
        {
            return @this.keys[@this.length - 1].time;
        }
    }
}