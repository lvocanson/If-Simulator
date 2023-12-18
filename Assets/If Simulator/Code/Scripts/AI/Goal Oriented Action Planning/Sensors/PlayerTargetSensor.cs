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
        public override void Created()
        {
        }
        public override void Update()
        {  
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(agent.transform.position, AttackConfig.SensorRadius, AttackConfig.AttackableLayerMask);

            if (hitColliders.Length > 0)
            {
                Collider2D closestCollider = FindClosestCollider(agent.transform.position, hitColliders);
                return new TransformTarget(closestCollider.transform);
            }

            return null;
        }

        private Collider2D FindClosestCollider(Vector2 position, Collider2D[] colliders)
        {
            Collider2D closestCollider = colliders[0];
            float closestDistance = Vector2.Distance(position, closestCollider.transform.position);

            for (int i = 1; i < colliders.Length; i++)
            {
                float distance = Vector2.Distance(position, colliders[i].transform.position);
                if (distance < closestDistance)
                {
                    closestCollider = colliders[i];
                    closestDistance = distance;
                }
            }

            return closestCollider;
        }

        public void Inject(DependencyInjector injector)
        {
            AttackConfig = injector.AttackConfig;
        }
    }
}
