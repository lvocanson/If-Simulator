using System;
using UnityEngine;

namespace Ability
{
    public class ShockWaveBehavior : AbilityActive
    {
        [Header("References")]
        [SerializeField] private GameObject _swPrefab;
        [SerializeField] private Transform _spawnPoint;
        
        [Header("Ability Settings")]
        private float _timer;
        [SerializeField] private AnimationCurve _evolutionCurve;
        [SerializeField] private float _maxSize;
        
        [Header("Ability Damage Management")]
        [SerializeField] private LayerMask _layers; //proj layer
        [SerializeField] private LayerMask _damageableEntityLayers; // enemy layer
        
        protected int _ownerLayer;
        public event Action OnHit;
        
        private GameObject _swInstance;

        private void OnTriggerEnter2D(Collider2D other)
        {
            int otherLayer = other.gameObject.layer;
            
            // Skip unwanted layers
            if (((1 << otherLayer) & _layers.value) == 0) return;
            if (otherLayer == _ownerLayer) return;
            
            if (otherLayer == _layers.value)
                Destroy(other.gameObject);
            else if (otherLayer == _damageableEntityLayers.value)
            {
                if (other.TryGetComponent(out Rigidbody2D rb))
                {
                    Vector2 dir = (other.transform.position - transform.position).normalized;
                    rb.AddForce(dir * 1000, ForceMode2D.Impulse);
                }
            }
        }
        
        protected override void OnEffectStart()
        {
            _swInstance = Instantiate(_swPrefab, _spawnPoint.position, Quaternion.identity);
            _timer = 0;
        }

        protected override void OnEffectUpdate()
        {
            _timer += Time.fixedDeltaTime / _abilitySo.AbilityDuration;
            float power = _evolutionCurve.Evaluate(_timer);
            _swInstance.transform.localScale = Vector3.one * (power * _maxSize);
        }

        protected override void OnEffectEnd()
        {
            Destroy(_swInstance);
        }
    }
}