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
            bullet.transform.rotation = _bulletSpawnPoint.rotation * Quaternion.Euler(0,0,90);
            bullet.SetActive(true);
            bullet.GetComponent<Projectile>().Initialize(gameObject.layer, _bulletSpawnPoint.up, true);
        }

        protected override void OnBulletReturnToPool(GameObject bullet) => bullet.SetActive(false);

        protected override void OnBulletDestroy(GameObject bullet) => Destroy(bullet);
    }
}