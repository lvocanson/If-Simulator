using UnityEngine;

namespace Ability
{
    public class AbilityPrimaryShoot : AbilityShoot
    {
        protected override GameObject CreateBullet()
        {
            // Spawn a new bullet at the spawn point (player position)
            GameObject bp = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation, _bulletContainer);

            var bulletBehavior = bp.GetComponent<Projectile>();
            bulletBehavior.Initialize(gameObject.layer, _bulletSpawnPoint.up, true);
            bulletBehavior.SetDamage(RuntimeAbilitySo.Value);
            bulletBehavior.OnDestroy += CleanProjectile;

            return bp;
        }

        private void CleanProjectile(Projectile p)
        {
            if (p.gameObject.activeSelf is true)
                _bulletPool.Release(p.gameObject);
        }

        protected override void OnBulletTakeFromPool(GameObject bullet)
        {
            base.OnBulletTakeFromPool(bullet);
            
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = _bulletSpawnPoint.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Projectile>().Initialize(gameObject.layer, _bulletSpawnPoint.up, true);
        }

        protected override void OnBulletReturnToPool(GameObject bullet)
        {
            base.OnBulletReturnToPool(bullet);
            
            bullet.SetActive(false);
        }

        protected override void OnBulletDestroy(GameObject bullet) => Destroy(bullet);
    }
}