using UnityEngine;

namespace Ability
{
    public static class AnimationCurveExtension
    {
        public static float Duration(this AnimationCurve @this)
        {
            return @this.keys[@this.length - 1].time;
        }
    }
    
    public class DashBehavior : AbilityActive
    {
        [Header("References")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Rigidbody2D _rb;

        [Header("Settings")]
        [SerializeField] private AnimationCurve _dashPower;

        private float _start;
        
        protected override void OnEffectStart()
        {
            _start = Time.time;
        }
        
        protected override void OnEffectUpdate()
        {
            float time = Time.time - _start;
            float power = _dashPower.Evaluate(time);
            _rb.velocity = _playerMovement.MovementValue * power;
        }

        protected override void OnEffectEnd()
        {
            
        }
    }
}