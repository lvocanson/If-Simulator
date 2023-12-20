using UnityEngine;

namespace Ability
{
    public class AbilityTurret : AbilityActive
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _spawnPoint;
        
        private TurretBrain _instance;
        
        protected override void OnEffectStart()
        {
            GameObject go = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
            _instance = go.GetComponent<TurretBrain>();
            _instance.So = _abilitySo;
        }

        protected override void OnEffectUpdate()
        {
        }

        protected override void OnEffectEnd()
        {
            _instance.OnDeath();
        }
    }
}