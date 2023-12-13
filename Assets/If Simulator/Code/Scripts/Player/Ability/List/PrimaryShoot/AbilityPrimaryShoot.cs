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
            EnemyDamage damageZone = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation).GetComponent<EnemyDamage>();
            damageZone.IgnoreLayer(gameObject.layer);
            Debug.Log(gameObject.layer);
        }

        protected override void OnEffectEnd()
        {
        }
    }
}