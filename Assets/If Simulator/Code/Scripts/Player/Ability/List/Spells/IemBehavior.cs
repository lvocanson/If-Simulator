using UnityEngine;

namespace Ability
{
    public class IemBehavior : AbilityActive
    {
        [SerializeField] private GameObject _iemPrefab;
        [SerializeField] private Transform _spawnPoint;
        
        [SerializeField] private float _speed = 10f;
        
        private GameObject _iemInstance;

        private void OnTriggerEnter(Collider other)
        {
            
        }
        
        protected override void OnEffectStart()
        {
            Debug.Log("Shockwave: OnEffectStart");
            _iemInstance = Instantiate(_iemPrefab, _spawnPoint.position, Quaternion.identity);
        }

        protected override void OnEffectUpdate()
        {
            _iemInstance.transform.localScale += Vector3.one * (_speed * Time.deltaTime); 
        }

        protected override void OnEffectEnd()
        {
            Destroy(_iemInstance);
        }
    }
}