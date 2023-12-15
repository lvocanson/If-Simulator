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

        private Coroutine _routine;

        private AbilityState _state = AbilityState.READY;

        public float CurCooldown
        {
            get => _curCooldown;
            set => _curCooldown = Mathf.Clamp(value, 0, _abilitySo.Cooldown);
        }

        public float CurActiveCooldown { get; private set; }

        private float GetActiveCooldown()
        {
            return _abilitySo.IsHoldable is true ? _abilitySo.Delay : _abilitySo.AbilityDuration;
        }

        // Called when the ability is activated (corresponding key pressed)
        // Note: This method does not start the cooldown right away, the cooldown is started when the ability's active time is over
        public sealed override void TryActivate()
        {
            // If the ability is not ready, do nothing
            if (_state != AbilityState.READY) return;
            
            // Mark the ability as active
            _state = AbilityState.ACTIVE;
            CurActiveCooldown = GetActiveCooldown();
            
            _routine = StartCoroutine(Routine());
            return;

            // Local function to start the start coroutine
            IEnumerator Routine()
            {
                while (true)
                {
                    OnEffectStart();
                    yield return new WaitForSeconds(CurActiveCooldown);
                    
                    // If the ability is not holdable, end it
                    if (_abilitySo.IsHoldable is true) continue;
                    
                    End();
                    yield break;
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
                    if (CurActiveCooldown > 0)
                    {
                        if (_abilitySo.IsHoldable is false) CurActiveCooldown -= Time.deltaTime;
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
        
        // Called every frame during the active time
        protected abstract void OnEffectUpdate();
        
        // Called once when the spell ends, the spell will go on cooldown after this method is called
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