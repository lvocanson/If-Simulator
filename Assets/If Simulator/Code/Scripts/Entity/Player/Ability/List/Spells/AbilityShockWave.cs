using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ability
{
    public class AbilityShockWave : AbilityActive
    {
        [Header("References")]
        [SerializeField] private GameObject _swPrefab;
        [SerializeField] private Transform _spawnPoint;
        
        private AbilityExplosionBehavior _swInstance;
        
        [Header("Feedback")]
        [SerializeField] private AudioSource _shockwaveAudioSource;
        [SerializeField] private AudioClip _shockwaveAudioClip;
        [SerializeField] private GameObject _particles; 
        
        protected override void OnEffectStart()
        {
            GameObject swGo = Instantiate(_swPrefab, _spawnPoint.position, Quaternion.identity);
            
            _swInstance = swGo.GetComponent<AbilityExplosionBehavior>();
            _swInstance.Init(RuntimeAbilitySo, this);
            
            _shockwaveAudioSource.PlayOneShot(_shockwaveAudioClip);
            Instantiate(_particles, transform.position, Quaternion.identity); 
        }

        protected override void OnEffectUpdate()
        {
            _swInstance.OnUpdate();
        }

        protected override void OnEffectEnd()
        {
            Destroy(_swInstance.gameObject);
        }
    }
}