using FiniteStateMachine;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Pool;

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
        [SerializeField] private SpriteRenderer _laserSight;
        [SerializeField, ReadOnly] private Transform _target;
        [SerializeField, ReadOnly] private ObjectPool<GameObject> _bulletPool;
        [SerializeField, ReadOnly] private float _attackDelay;
        
        private float nextAttackTimestamp = 0;
        

        public override void Enter(StateMachine manager, params object[] args)
        {
            base.Enter(manager, args);
            
            _target = (Transform)Args[0];
            _attackDelay = (float)Args[1];
            _bulletPool = (ObjectPool<GameObject>)Args[2];
        }
        
        private void Shoot()
        {
            _bulletPool.Get();
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }
            
            if (Time.time >= nextAttackTimestamp)
            {
                Shoot();
                nextAttackTimestamp = Time.time + _attackDelay;
            }
            
            float angle = Vector2.SignedAngle(Vector2.right, _target.position - transform.position) - 90;
            _rb.MoveRotation(angle);
            
            _laserSight.size = new Vector2(_laserSight.size.x, Vector2.Distance(transform.position, _target.position));
        }
    }
}