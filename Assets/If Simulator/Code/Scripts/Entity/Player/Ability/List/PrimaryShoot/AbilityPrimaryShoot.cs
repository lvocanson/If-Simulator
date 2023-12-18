﻿using UnityEngine;
using UnityEngine.Pool;

namespace Ability
{
    public class AbilityPrimaryShoot : AbilityActive
    {
        [Header("References")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private Transform _bulletContainer;

        [Header("Settings")]
        [SerializeField] private int _numberOfBulletsPerDefault;
        [SerializeField] private int _numberOfBulletsMax;

        private ObjectPool<GameObject> _bulletPool;

        private void Awake()
        {
            _bulletPool = new ObjectPool<GameObject>(CreateBullet, OnBulletTakeFromPool,
                OnBulletReturnToPool, OnBulletDestroy, true, _numberOfBulletsPerDefault, _numberOfBulletsMax);
        }

        protected override void OnEffectStart()
        {
            _bulletPool.Get();
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
            Projectile bp = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation, _bulletContainer).GetComponent<Projectile>();

            bp.Initialize(gameObject.layer, _bulletSpawnPoint.up, true);
            bp.SetDamage(_abilitySo.Damage);
            bp.OnDestroy += () => _bulletPool.Release(bp.gameObject);

            return bp.gameObject;
        }

        private void OnBulletTakeFromPool(GameObject bullet)
        {
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = _bulletSpawnPoint.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Projectile>().Initialize(gameObject.layer, _bulletSpawnPoint.up, true);

        }

        private void OnBulletReturnToPool(GameObject bullet) => bullet.SetActive(false);

        private void OnBulletDestroy(GameObject bullet) => Destroy(bullet);
    }
}