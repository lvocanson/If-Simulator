using UnityEngine;

namespace Ability
{
    
    public class DashBehavior : AbilityActive
    {
        [Header("References")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Rigidbody2D _rb;

        private float _start;
        
        protected override void OnEffectStart()
        {
            _start = Time.time;
        }
        
        protected override void OnEffectUpdate()
        {
            float time = Time.time - _start;
            float power = _abilitySo.EvolutionCurve.Evaluate(time);
            _rb.velocity = _playerMovement.MovementValue * power;
        }

        protected override void OnEffectEnd()
        {
            
        }
    }
    
    

}