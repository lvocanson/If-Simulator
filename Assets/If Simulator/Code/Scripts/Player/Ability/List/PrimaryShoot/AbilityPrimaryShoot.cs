using System.Collections;
using UnityEngine;

namespace Ability.List
{
    public class AbilityPrimaryShoot : AbilityActive
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        
        protected override void OnEffectStart()
        {
            GameObject bp = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            var bulletBehavior = bp.GetComponent<BulletBehavior>();
            bulletBehavior.Damage = _abilitySo.AbilityDamage;
        }

        protected override void OnEffectUpdate()
        {
            
        }

        protected override void OnEffectEnd()
        {
        }
    }
}