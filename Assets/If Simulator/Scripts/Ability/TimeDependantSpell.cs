using UnityEngine;

namespace Ability
{
    public class TimeDependantSpell : MonoBehaviour
    {
        protected enum AbilityState
        {
            READY,
            ACTIVE,
            COOLDOWN
        }

        // Stats of this ability, each ability has its own stats
        public AbilityScriptableObject AbilitySo;

        public float CurCooldown
        {
            get => _curCooldown;
            set => _curCooldown = Mathf.Clamp(value, 0, AbilitySo.AbilityCooldown);
        }
        
        public float CurActiveCooldown
        {
            get => _curActiveCooldown;
            set => _curActiveCooldown = Mathf.Clamp(value, 0, AbilitySo.AbilityActiveCooldown);
        }

        // Called when the ability is activated (corresponding key pressed)
        // Note: This method does not start the cooldown right away, the cooldown is started when the ability's active time is over
        public virtual void Activate()
        {
            _state = AbilityState.ACTIVE;
            _curActiveCooldown = AbilitySo.AbilityActiveCooldown;
        }

        private void Update()
        {
            switch (_state)
            {
                case AbilityState.ACTIVE:
                {
                    if (_curActiveCooldown > 0) _curActiveCooldown -= Time.deltaTime;
                    else End();
                    break;
                }
                case AbilityState.COOLDOWN:
                {
                    if (_curCooldown > 0) _curCooldown -= Time.deltaTime;
                    else _state = AbilityState.READY;
                    break;
                }
                case AbilityState.READY:
                default:
                    break;
            }
        }
            
        public void LevelUp()
        {
            if (_curLevel < AbilitySo.AbilityMaxLevel) _curLevel++;
        }

        protected virtual void End()
        {
            _state = AbilityState.COOLDOWN;
            _curCooldown = AbilitySo.AbilityCooldown;
        }

        protected float _curCooldown;
        protected float _curActiveCooldown;
        
        protected AbilityState _state = AbilityState.READY;
        
        protected ushort _curLevel;
    }
}