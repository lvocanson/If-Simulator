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
        }

        protected override void OnEffectUpdate()
        {
            //if (!_isShooting) return;
            EntityDamage damageZone = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation).GetComponent<EntityDamage>();
            damageZone.IgnoreLayer(gameObject.layer);
        }

        protected override void OnEffectEnd()
        {
        }
    }
}