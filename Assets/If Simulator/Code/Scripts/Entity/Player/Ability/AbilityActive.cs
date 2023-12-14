using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


namespace Ability
{
    public abstract class AbilityActive : AbilityBase<SoAbilityCooldown>
    {
        private enum AbilityState
        {
            READY,
            ACTIVE,
            COOLDOWN
        }

        private float _curCooldown;
        private float _curActiveCooldown;

        private Coroutine _routine;

        private AbilityState _state = AbilityState.READY;

        public float CurCooldown
        {
            get => _curCooldown;
            set => _curCooldown = Mathf.Clamp(value, 0, _abilitySo.Cooldown);
        }

        public float CurActiveCooldown
        {
            get => _curActiveCooldown;
            set => _curActiveCooldown = Mathf.Clamp(value, 0, _abilitySo.ActiveCooldown);
        }

        // Called when the ability is activated (corresponding key pressed)
        // Note: This method does not start the cooldown right away, the cooldown is started when the ability's active time is over
        public sealed override void TryActivate()
        {
            // If the ability is not ready, do nothing
            if (_state != AbilityState.READY) return;
            
            // Mark the ability as active
            _state = AbilityState.ACTIVE;
            _curActiveCooldown = _abilitySo.ActiveCooldown;
            
            _routine = StartCoroutine(Routine());
            return;

            // Local function to start the start coroutine
            IEnumerator Routine()
            {
                while (true)
                {
                    OnEffectStart();

                    yield return new WaitForSeconds(_abilitySo.ActiveCooldown);
                    
                    // If the ability is not holdable, end it
                    if (_abilitySo.IsHoldable is false) End();
                }
            }
        }

        public sealed override void LevelUp() => CurrentLevel = (ushort)Mathf.Clamp(CurrentLevel + 1, 0, _abilitySo.MaxLevel);

        private void Update()
        {
            switch (_state)
            {
                // If the ability is on cooldown, decrease the cooldown
                case AbilityState.COOLDOWN:
                {
                    if (_curCooldown > 0) _curCooldown -= Time.deltaTime;
                    else _state = AbilityState.READY;
                    break;
                }
                // If the ability is active, decrease the active cooldown and call the update method
                case AbilityState.ACTIVE:
                {
                    if (_curActiveCooldown > 0)
                    {
                        _curActiveCooldown -= Time.deltaTime;
                        OnEffectUpdate();
                    }
                    break;
                }
                case AbilityState.READY:
                default:
                    break;
            }
        }

        // Called once when the click is started
        protected abstract void OnEffectStart();
        
        // Called every active cooldown time
        protected abstract void OnEffectUpdate();
        
        // Called once if the ability is not holdable or TO CALL when the click is released if the ability is holdable
        protected abstract void OnEffectEnd();

        public sealed override void End()
        {
            // Prevent ending the ability if it is already on cooldown
            if (_state == AbilityState.COOLDOWN) return;
            
            // Enter cooldown state
            _state = AbilityState.COOLDOWN;
            _curCooldown = _abilitySo.Cooldown;

            // If the ability is active, stop the coroutine
            if (_routine != null)
            {
                StopCoroutine(_routine);
                _routine = null;
            }

            OnEffectEnd();
        }
    }
}