using UnityEngine;
using UnityEngine.Pool;

namespace Ability
{
    public abstract class AbilityShoot : AbilityActive
    {
        [Header("References")]
        [SerializeField] protected GameObject _bulletPrefab;
        [SerializeField] protected Transform _bulletSpawnPoint;
        [SerializeField] protected Transform _bulletContainer;

        [Header("Settings")]
        [SerializeField] protected int _numberOfBulletsPerDefault;
        [SerializeField] protected int _numberOfBulletsMax;

        protected ObjectPool<GameObject> _bulletPool;
        
        protected override void Awake()
        {
            base.Awake();
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

        protected abstract GameObject CreateBullet();

        protected virtual void OnBulletTakeFromPool(GameObject bullet)
        {
            Projectile proj = bullet.GetComponent<Projectile>();
            proj.OnEntityKill += TriggerEnemyKilled;
        }

        protected virtual void OnBulletReturnToPool(GameObject bullet)
        {
            Projectile proj = bullet.GetComponent<Projectile>();
            proj.OnEntityKill -= TriggerEnemyKilled;
        }
        
        protected abstract void OnBulletDestroy(GameObject bullet);
    }
}