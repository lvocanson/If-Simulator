using System;
using FiniteStateMachine;
using UnityEngine;

namespace Behaviors
{
    public class TurretSeek : BaseState
    {
        [SerializeField] private Transform _root;

        private void Update()
        {
            _root.Rotate(Vector3.forward, 10f * Time.deltaTime);
        }
    }
}