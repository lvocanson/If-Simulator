using UnityEngine;

namespace Ability.List.Spells
{
    public class Shockwave : AbilityActive
    {
        [SerializeField] private GameObject _shockwavePrefab;
        [SerializeField] private Transform _shockwaveSpawnPoint;
        
        protected override void OnEffectStart()
        {
            Debug.Log("Shockwave: OnEffectStart");
            Instantiate(_shockwavePrefab, _shockwaveSpawnPoint.position, Quaternion.identity);
        }

        protected override void OnEffectUpdate()
        {
            Debug.Log("Shockwave: OnEffectUpdate");
        }

        protected override void OnEffectEnd()
        {
            Debug.Log("Shockwave: OnEffectEnd");
        }
    }
}