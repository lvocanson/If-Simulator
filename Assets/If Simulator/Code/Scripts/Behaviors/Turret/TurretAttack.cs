using System;
using System.Collections;
using Ability;
using FiniteStateMachine;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Pool;
using Utility;

namespace Behaviors
{
    public class TurretAttack : BaseState
    {
        /*
         * Args 0: Transform target
         * Args 1: Attack delay
         * Args 2: ObjectPool<GameObject> bulletPool
         */

        [SerializeField] private Rigidbody2D _rb;
        
        [SerializeField, ReadOnly] private Transform _target;
        [SerializeField, ReadOnly] private ObjectPool<GameObject> _bulletPool;
        [SerializeField, ReadOnly] private float _attackDelay;
        
        private Coroutine _attackCoroutine;

        public override void Enter(StateMachine manager, params object[] args)
        {
            base.Enter(manager, args);
            
            _target = (Transform)Args[0];
            _attackDelay = (float)Args[1];
            _bulletPool = (ObjectPool<GameObject>)Args[2];
            _attackCoroutine = StartCoroutine(Attack());

        }

        public override void Exit()
        {
            base.Exit();
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }
        }

        private void Shoot()
        {
            _bulletPool.Get();
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                Shoot();
                yield return new WaitForSeconds(_attackDelay);
            }
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }
            
            float angle = Vector2.SignedAngle(Vector2.right, _target.position - transform.position) - 90;
            _rb.MoveRotation(angle);
        }
    }
}