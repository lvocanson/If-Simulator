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
        private Collider2D[] colliders2D = new Collider2D[1];
        public override void Created()
        {
            
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            //Collider2D[] colliders2D = Physics2D.OverlapCircleAll(agent.transform.position, AttackConfig.SensorRadius, AttackConfig.AttackableLayerMask);

            //if (colliders2D.Length > 0)
            //{
            //    return new TransformTarget(colliders2D[0].transform);
            //}

            return null;
        }

        public override void Update()
        {
            
        }

        //public void Inject(DependencyInjector injector)
        //{
        //    AttackConfig = injector.AttackConfig;
        //}
    }

}
