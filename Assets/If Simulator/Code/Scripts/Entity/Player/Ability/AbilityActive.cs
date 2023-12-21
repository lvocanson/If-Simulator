using System;
using System.Collections;
using UnityEngine;

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
        public event Action<float, float> OnCooldownChanged;
        public event Action OnAbilityActivated;

        private Coroutine _routine;

        private AbilityState _state = AbilityState.READY;
        
        private float CurActiveCooldown { get; set; }

        private float GetActiveCooldown()
        {
            return RuntimeAbilitySo.IsHoldable is true ? RuntimeAbilitySo.Delay : RuntimeAbilitySo.AbilityDuration;
        }
        
        public event Action OnEnemyKilled;
        
        public void TriggerEnemyKilled()
        {
            OnEnemyKilled?.Invoke();
        }

        // Called when the ability is activated (corresponding key pressed)
        // Note: This method does not start the cooldown right away, the cooldown is started when the ability's active time is over
        public sealed override void TryActivate()
        {
            // If the ability is not ready, do nothing
            if (_state != AbilityState.READY) return;

            // Mark the ability as active
            _state = AbilityState.ACTIVE;
            OnAbilityActivated?.Invoke();
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
                    if (RuntimeAbilitySo.IsHoldable is true) continue;

                    End();
                    yield break;
                }
            }
        }

        public sealed override void LevelUp()
        {
            CurrentLevel = (ushort)Mathf.Clamp(CurrentLevel + 1, 0, RuntimeAbilitySo.MaxLevel);
            
            // If the ability is on cooldown, reset the cooldown
            if (_state == AbilityState.COOLDOWN)
            {
                _state = AbilityState.READY;
                _curCooldown = 0;
            }
            
            // Increase the ability's stats
            RuntimeAbilitySo.LevelUp();
        }

        private void Update()
        {
            switch (_state)
            {
                // If the ability is on cooldown, decrease the cooldown
                case AbilityState.COOLDOWN:
                {
                    if (_curCooldown > 0)
                    {
                        _curCooldown -= Time.deltaTime;
                        OnCooldownChanged?.Invoke(_curCooldown, RuntimeAbilitySo.Cooldown);
                    }
                    else
                    {
                        _state = AbilityState.READY;
                    }
                    break;
                }
                // If the ability is active, decrease the active cooldown and call the update method
                case AbilityState.ACTIVE:
                {
                    if (CurActiveCooldown > 0)
                    {
                        if (RuntimeAbilitySo.IsHoldable is false) CurActiveCooldown -= Time.deltaTime;
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
            _curCooldown = RuntimeAbilitySo.Cooldown;

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
