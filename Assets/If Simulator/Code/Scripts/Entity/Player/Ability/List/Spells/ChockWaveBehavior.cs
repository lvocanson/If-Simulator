using UnityEngine;

namespace Ability
{
    public class ChockWaveBehavior : AbilityActive
    {
        [Header("References")]
        [SerializeField] private GameObject _swPrefab;
        [SerializeField] private Transform _spawnPoint;
        
        private float _start;
        [SerializeField] private AnimationCurve _evolutionCurve;
        [SerializeField] private float _maxSize;
        
        private GameObject _swInstance;

        private void OnTriggerEnter(Collider other)
        {
            
        }
        
        protected override void OnEffectStart()
        {
            _swInstance = Instantiate(_swPrefab, _spawnPoint.position, Quaternion.identity);
            _start = Time.time;
        }

        protected override void OnEffectUpdate()
        {
            float time = Time.time - _start;
            float evolutionOverTime = _evolutionCurve.Evaluate(time);
            _swInstance.transform.localScale = Vector3.one * (evolutionOverTime * _maxSize);
        }

        protected override void OnEffectEnd()
        {
            Destroy(_swInstance);
        }
    }
}