using CrashKonijn.Goap.Classes;

using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;
using IfSimulator.GOAP.Config;
using UnityEngine;

namespace IfSimulator.GOAP.Sensors
{
    public class PlayerTargetSensor : LocalTargetSensorBase, IInjectable
    {
        private AttackConfigSO AttackConfig;
        private Collider2D[] _colliders = new Collider2D[1];
        public override void Created()
        {
        }
        public override void Update()
        {  
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            if (Physics2D.OverlapCircleNonAlloc(agent.transform.position, AttackConfig.SensorRadius, 
                _colliders, AttackConfig.AttackableLayerMask) > 0)
            {
                return new TransformTarget(_colliders[0].transform);
            }

            return null;
        }

        public void Inject(DependencyInjector injector)
        {
            AttackConfig = injector.AttackConfig;
        }
    }
}
