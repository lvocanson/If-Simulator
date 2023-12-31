﻿using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ability
{
    public class DashBehavior : AbilityActive
    {
        [Header("References")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Rigidbody2D _rb;
        
        private float _timer;
        
        [Header("Increase properties")]
        [SerializeField] private AnimationCurve _evolutionCurve;
        [SerializeField] private float _maxSpeed;

        [Header("Decrease properties")]
        [SerializeField] private AnimationCurve _velocityReleaseCurve;
        [SerializeField] private float _velocityReleaseTime = 0.2f;
        
        [Header("Feedback")]
        [SerializeField] private AudioSource _dashAudioSource;
        [SerializeField] private AudioClip _dashAudioClip;
        
        protected override void OnEffectStart()
        {
            _timer = 0;
            _playerMovement.IsControllable = false;
            _dashAudioSource.PlayOneShot(_dashAudioClip);
        }
        
        protected override void OnEffectUpdate()
        {
            _timer += Time.fixedDeltaTime / RuntimeAbilitySo.AbilityDuration;
            float power = _evolutionCurve.Evaluate(_timer);
            Vector2 direction = (_rb.velocity == Vector2.zero) ? _playerMovement.transform.up : _rb.velocity.normalized;
            _rb.velocity = direction * (power * _maxSpeed);
        }

        protected override void OnEffectEnd()
        {
            StartCoroutine(OnDashEnd());
            return;
            
            IEnumerator OnDashEnd()
            {
                float timer = 0;
                while (timer < 1 && _playerMovement.UnclampedMovementValue.sqrMagnitude < _rb.velocity.sqrMagnitude)
                {
                    timer += Time.fixedDeltaTime / _velocityReleaseTime;
                    float power = _velocityReleaseCurve.Evaluate(timer);
                    _rb.velocity = Vector2.Lerp(_rb.velocity, _playerMovement.UnclampedMovementValue, power);
                    yield return new WaitForFixedUpdate();
                }
                _playerMovement.IsControllable = true;
            }
        }
    }
}