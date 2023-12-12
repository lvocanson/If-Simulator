using UnityEngine;
using UnityEngine.Serialization;

namespace Ability
{
    public abstract class AbilityActive : Ability
    {
        private enum AbilityState
        {
            READY,
            ACTIVE,
            COOLDOWN
        }

        [SerializeField] private SoAbilityCooldown _abilitySo; 
        
        private float _curCooldown;
        private float _curActiveCooldown;
        
        private AbilityState _state = AbilityState.READY;
        
        public float CurCooldown
        {
            get => _curCooldown;
            set => _curCooldown = Mathf.Clamp(value, 0, _abilitySo.AbilityCooldown);
        }
        
        public float CurActiveCooldown
        {
            get => _curActiveCooldown;
            set => _curActiveCooldown = Mathf.Clamp(value, 0, _abilitySo.AbilityActiveCooldown);
        }

        // Called when the ability is activated (corresponding key pressed)
        // Note: This method does not start the cooldown right away, the cooldown is started when the ability's active time is over
        public override void TryActivate()
        {
            if (_state != AbilityState.READY) return;
            
            _state = AbilityState.ACTIVE;
            OnEffectStart();
            _curActiveCooldown = _abilitySo.AbilityActiveCooldown;
        }
        
        public override void LevelUp()
        {
            throw new System.NotImplementedException();
        }
        
        private void Update()
        {
            switch (_state)
            {
                case AbilityState.ACTIVE:
                {
                    if (_curActiveCooldown > 0)
                    {
                        _curActiveCooldown -= Time.deltaTime;
                        OnEffectUpdate();
                    }
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

        protected abstract void OnEffectStart();
        protected abstract void OnEffectUpdate();
        protected abstract void OnEffectEnd();
        
        protected sealed override void End()
        {
            _state = AbilityState.COOLDOWN;
            _curCooldown = _abilitySo.AbilityCooldown;
            OnEffectEnd();
        }
    }
}