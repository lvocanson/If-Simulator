using UnityEngine;
using UnityEngine.Pool;

namespace Ability
{
    public class AbilityPrimaryShoot : AbilityActive
    {
        [Header("Components")] 
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private Transform _bulletContainer;

        [Header("Settings")] 
        [SerializeField] private int _numberOfBulletsPerDefault;
        [SerializeField] private int _numberOfBulletsMax;

        private ObjectPool<GameObject> _bulletPool;

        private void Awake()
        {
            _bulletPool = new ObjectPool<GameObject>(CreateBullet, OnBulletTakeFromPool, OnBulletReturnToPool, OnBulletDestroy, true, _numberOfBulletsPerDefault,
                _numberOfBulletsMax);
        }

        protected override void OnEffectStart()
        {
            GameObject bullet = _bulletPool.Get();
        }

        protected override void OnEffectUpdate()
        {
        }

        protected override void OnEffectEnd()
        {
        }

        private GameObject CreateBullet()
        {
            // Spawn a new bullet at the spawn point (player position)
            GameObject bp = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation, _bulletContainer);

            var bulletBehavior = bp.GetComponent<BulletBehavior>();
            bulletBehavior.Initialize(gameObject.layer, _bulletSpawnPoint.up);
            bulletBehavior.SetDamage(_abilitySo.Damage);
            bulletBehavior.OnDestroy += () => _bulletPool.Release(bp);

            return bp;
        }

        private void OnBulletTakeFromPool(GameObject bullet)
        {
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = _bulletSpawnPoint.rotation;
            bullet.SetActive(true);
        }

        private void OnBulletReturnToPool(GameObject bullet) => bullet.SetActive(false);

        private void OnBulletDestroy(GameObject bullet) => Destroy(bullet);
    }
}