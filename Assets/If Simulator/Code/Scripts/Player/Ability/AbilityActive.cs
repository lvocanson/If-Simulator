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
            COOLDOWN
        }

        private float _curCooldown;
        private float _curActiveCooldown;

        private Coroutine _routine;

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
        public sealed override void TryActivate()
        {
            if (_state != AbilityState.READY) return;

            _routine = StartCoroutine(Shoot());
            return;

            IEnumerator Shoot()
            {
                OnEffectStart();
                float startTime = Time.time;
                while (true)
                {
                    OnEffectUpdate();

                    yield return new WaitForSeconds(_abilitySo.AbilityActiveCooldown);
                }
            }
        }

        public sealed override void LevelUp() => CurrentLevel = (ushort)Mathf.Clamp(CurrentLevel + 1, 0, _abilitySo.AbilityMaxLevel);

        private void Update()
        {
            Debug.Log(_state);
            switch (_state)
            {
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

        public sealed override void End()
        {
            if (_state == AbilityState.COOLDOWN) return;
            _state = AbilityState.COOLDOWN;
            _curCooldown = _abilitySo.AbilityCooldown;

            if (_routine != null)
            {
                StopCoroutine(_routine);
                _routine = null;
            }

            OnEffectEnd();
        }
    }
}