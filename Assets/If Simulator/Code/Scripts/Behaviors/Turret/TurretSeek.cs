using System;
using FiniteStateMachine;
using UnityEngine;

namespace Behaviors
{
    public class TurretSeek : BaseState
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _rotationSpeed;

        private void Update()
        {
            _root.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
    }
}