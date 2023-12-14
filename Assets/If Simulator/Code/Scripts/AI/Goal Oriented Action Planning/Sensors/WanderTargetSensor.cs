using UnityEngine;
using CrashKonijn.Goap.Sensors;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Classes;
using UnityEngine.AI;

namespace IfSimulator.GOAP.Sensors
{

    public class WanderTargetSensor : LocalTargetSensorBase
    {
        public override void Created()
        {
        }

        public override void Update()
        {
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            Vector3 position = GetRandomPosition(agent);

            return new PositionTarget(position);
        }

        private Vector3 GetRandomPosition(IMonoAgent agent)
        {
            int count = 0;

            while (count < 5)
            {

                Vector3 position = agent.transform.position + Random.insideUnitSphere * 5.0f;

                if (NavMesh.SamplePosition(position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                {
                    return hit.position;
                }

                count++;
            }

            return agent.transform.position;
        }
    }
}
