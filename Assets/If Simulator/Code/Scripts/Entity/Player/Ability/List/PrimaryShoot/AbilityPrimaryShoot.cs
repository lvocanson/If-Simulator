using UnityEngine;

namespace Ability
{
    public class AbilityPrimaryShoot : AbilityShoot
    {
        [SerializeField] private AudioClip _shootSound;
        
        protected override GameObject CreateBullet()
        {
            // Spawn a new bullet at the spawn point (player position)
            GameObject bp = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation, _bulletContainer);

            var bulletBehavior = bp.GetComponent<Projectile>();
            bulletBehavior.Initialize(gameObject.layer, _bulletSpawnPoint.up, true);
            bulletBehavior.SetDamage(RuntimeAbilitySo.Value);
            bulletBehavior.OnDestroy += CleanProjectile;
            
            if (_shootSound != null)
                AudioSource.PlayClipAtPoint(_shootSound, transform.position);
            
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
            bullet.transform.rotation = _bulletSpawnPoint.rotation * Quaternion.Euler(0,0,90);
            bullet.SetActive(true);
            Projectile bulletBehavior = bullet.GetComponent<Projectile>();
            bulletBehavior.Initialize(gameObject.layer, _bulletSpawnPoint.up, true);
            bulletBehavior.SetDamage(RuntimeAbilitySo.Value);
            
            if (_shootSound != null)
                AudioSource.PlayClipAtPoint(_shootSound, transform.position);
        }

        protected override void OnBulletReturnToPool(GameObject bullet)
        {
            base.OnBulletReturnToPool(bullet);
            
            bullet.SetActive(false);
        }

        protected override void OnBulletDestroy(GameObject bullet) => Destroy(bullet);
    }
}