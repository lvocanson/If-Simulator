﻿using System;
using System.Collections.Generic;
using System.Linq;
using Behaviors;
using FiniteStateMachine;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Pool;

namespace Ability
{
    public class TurretBrain : StateMachine
    {
        [Header("States")] 
        [SerializeField] private TurretAttack _attackState;
        [SerializeField] private TurretSeek _seekState;
        [SerializeField] private TurretDestroy _destroyState;

        [Header("References")] 
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private Transform _bulletContainer;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _shootSound;

        [Header("Settings")] 
        [SerializeField] private int _numberOfBulletsPerDefault;
        [SerializeField] private int _numberOfBulletsMax;
        [SerializeField] private float _attackDelay;
        [SerializeField] private LayerMask _wallLayer;

        [Header("Debug")] 
        [SerializeField, ReadOnly] private readonly List<DamageableEntity> _closeEntities = new();
        [SerializeField, ReadOnly] private DamageableEntity _currentTarget;

        private ObjectPool<GameObject> _bulletPool;

        private void Awake()
        {
            _bulletPool = new ObjectPool<GameObject>(CreateBullet, OnBulletTakeFromPool,
                OnBulletReturnToPool, OnBulletDestroy, true, _numberOfBulletsPerDefault, _numberOfBulletsMax);
        }

        public AbilityTurret ParentAbility
        {
            get => _parentAbility;
            set
            {
                _parentAbility = value;
                _collider.radius = _parentAbility.RuntimeAbilitySo.Range;
            }
        }

        private AbilityTurret _parentAbility;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Assert that the entity is damageable
            if (!other.transform.parent) return;
            if (other.transform.parent.TryGetComponent(out DamageableEntity damageable) is false) return;
            
            // Assert that the entity is not already in the list
            if (_closeEntities.Contains(damageable) is true) return;
            
            // Assert that the entity is not behind a wall
            // This is done at the end because it is the most expensive operation
            if (Physics2D.Linecast(transform.position, other.transform.position, _wallLayer.value)) return;
            
            // Add the new entity to the list
            _closeEntities.Add(damageable);

            // If there is no target, set the current target to the new one and change state
            if (_currentTarget != null) return;

            _currentTarget = damageable;
            ChangeState(_attackState, other.gameObject.transform, _attackDelay, _bulletPool);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.transform.parent) return;
            if (other.transform.parent.TryGetComponent(out DamageableEntity damageable) is false) return;
            
            if (_closeEntities.Contains(damageable) is false) return;

            // Remove the entity from the list
            _closeEntities.Remove(damageable);
            if (_currentTarget != damageable) return;

            if (_closeEntities.Count > 0)
            {
                // Set the current target to the first entity in the list and change state
                _currentTarget = _closeEntities.First(i => i !=null);
                ChangeState(_attackState, _currentTarget.transform, _attackDelay, _bulletPool);
            }
            else
            {
                // If there is no more entity in the list, set the current target to null and go back to seek state
                _currentTarget = null;
                ChangeState(_seekState);
            }
        }

        public void OnDeath()
        {
            // Handle turret explosion
            _currentTarget = null;
            ChangeState(_destroyState, ParentAbility);

            // Destroy the game object
            Destroy(gameObject);
        }

        private GameObject CreateBullet()
        {
            // Spawn a new bullet at the spawn point (player position)
            GameObject bp = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation, _bulletContainer);

            var proj = bp.GetComponent<Projectile>();
            proj.Initialize(gameObject.layer, _bulletSpawnPoint.up);
            proj.SetDamage(_parentAbility.RuntimeAbilitySo.Value);
            proj.OnDestroy += CleanProjectile;

            if (_shootSound)
            {
                _audioSource.PlayOneShot(_shootSound);
            }

            return bp;
        }

        private void CleanProjectile(Projectile p)
        {
            _bulletPool.Release(p.gameObject);
        }

        private void OnBulletTakeFromPool(GameObject bullet)
        {
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = _bulletSpawnPoint.rotation;
            bullet.SetActive(true);

            var proj = bullet.GetComponent<Projectile>();
            proj.Initialize(gameObject.layer, _bulletSpawnPoint.up, managedByPool:true);
            proj.OnEntityKill += ParentAbility.TriggerEnemyKilled;
            
            if (_shootSound)
            {
                _audioSource.PlayOneShot(_shootSound);
            }
        }

        private void OnBulletReturnToPool(GameObject bullet)
        {
            Projectile proj = bullet.GetComponent<Projectile>();
            proj.OnEntityKill -= ParentAbility.TriggerEnemyKilled;
            bullet.SetActive(false);
        }

        private void OnBulletDestroy(GameObject bullet) => Destroy(bullet);
    }
}