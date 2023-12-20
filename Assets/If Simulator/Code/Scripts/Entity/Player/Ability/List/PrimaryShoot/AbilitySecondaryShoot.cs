﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Ability
{
    public class AbilitySecondaryShoot : AbilityShoot
    {
        [SerializeField] private GameObject _explodePrefab;
        [SerializeField] private SoAbilityCooldown _explodeSo;

        protected override GameObject CreateBullet()
        {
            // Spawn a new bullet at the spawn point (player position)
            GameObject bp = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation, _bulletContainer);

            var bulletBehavior = bp.GetComponent<Projectile>();
            bulletBehavior.Initialize(gameObject.layer, _bulletSpawnPoint.up, true);
            bulletBehavior.SetDamage(_abilitySo.Damage);
            bulletBehavior.OnDestroy += CleanProjectile;

            return bp;
        }
        
        private void CleanProjectile(Projectile p)
        {
            _bulletPool.Release(p.gameObject);
        }

        protected override void OnBulletTakeFromPool(GameObject bullet)
        {
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = _bulletSpawnPoint.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Projectile>().Initialize(gameObject.layer, _bulletSpawnPoint.up, true);
        }

        protected override void OnBulletReturnToPool(GameObject bullet)
        {
            var explosion = Instantiate(_explodePrefab, bullet.transform.position, Quaternion.identity).GetComponent<AbilityExplosionBehavior>();
            explosion.Init(_explodeSo);
            
            bullet.SetActive(false);
        }

        protected override void OnBulletDestroy(GameObject bullet)
        {
            Destroy(bullet);
        }
    }
}