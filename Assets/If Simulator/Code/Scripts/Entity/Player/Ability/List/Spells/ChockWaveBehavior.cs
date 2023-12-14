using UnityEngine;

namespace Ability
{
    public class ChockWaveBehavior : AbilityActive
    {
        [Header("References")]
        [SerializeField] private GameObject _swPrefab;
        [SerializeField] private Transform _spawnPoint;
        
        private float _start;
        
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
            float power = _abilitySo.EvolutionCurve.Evaluate(time);
            _swInstance.transform.localScale = Vector3.one * power;
        }

        protected override void OnEffectEnd()
        {
            Destroy(_swInstance);
        }
    }
}