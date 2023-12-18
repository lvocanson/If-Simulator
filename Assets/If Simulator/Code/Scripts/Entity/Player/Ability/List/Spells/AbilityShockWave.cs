﻿using System;
using UnityEngine;

namespace Ability
{
    public class AbilityShockWave : AbilityActive
    {
        [Header("References")]
        [SerializeField] private GameObject _swPrefab;
        [SerializeField] private Transform _spawnPoint;
        
        private ShockWaveBehavior _swInstance;
        
        protected override void OnEffectStart()
        {
            GameObject swGo = Instantiate(_swPrefab, _spawnPoint.position, Quaternion.identity);
            
            _swInstance = swGo.GetComponent<ShockWaveBehavior>();
            _swInstance.Init(_abilitySo.Damage);
        }

        protected override void OnEffectUpdate()
        {
            _swInstance.OnUpdate(_abilitySo.AbilityDuration);
        }

        protected override void OnEffectEnd()
        {
            Destroy(_swInstance.gameObject);
        }
    }
}