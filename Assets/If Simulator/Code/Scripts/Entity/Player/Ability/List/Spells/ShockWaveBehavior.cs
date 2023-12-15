using UnityEngine;

namespace Ability
{
    public class ShockWaveBehavior : AbilityActive
    {
        [Header("References")]
        [SerializeField] private GameObject _swPrefab;
        [SerializeField] private Transform _spawnPoint;
        
        private float _timer;
        [SerializeField] private AnimationCurve _evolutionCurve;
        [SerializeField] private float _maxSize;
        
        private GameObject _swInstance;

        private void OnTriggerEnter(Collider other)
        {
            
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