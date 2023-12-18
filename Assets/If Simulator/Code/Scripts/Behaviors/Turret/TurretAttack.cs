using System;
using System.Collections;
using Ability;
using FiniteStateMachine;
using NaughtyAttributes;
using UnityEngine;
using Utility;

namespace Behaviors
{
    public class TurretAttack : BaseState
    {
        /*
         * Args 0: Transform target
         * Args 1: Attack delay
         * Args 2: Attack damage
         * Args 3: ObjectPool<GameObject> bulletPool
         */

        [SerializeField] private Rigidbody2D _rb;
        [SerializeField, ReadOnly] private Transform _target;

        private void OnEnable()
        {
            _target = (Transform)Args[0];

            float angle = TransformUtility.AngleBetweenTwoPoints(_target.position, transform.position);
            //Debug.Log("Angle between turret and target: " + angle + " Target position: " + _target.position + " Turret position: " + transform.position);

            _rb.MoveRotation(angle);
        }

        private void MargeShoot()
        {
            // Vector3 direction = _target.transform.position - transform.position;
            // Projectile bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            // bullet.Initialize(gameObject.layer, direction);
        }

        private IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds((float)Args[1]);
                MargeShoot();
            }
        }
    }
}