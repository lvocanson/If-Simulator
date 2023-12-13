using UnityEngine;
using CrashKonijn.Goap.Sensors;
using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Classes;

namespace IfSimulator.GOAP.Sensors
{

    public class WanderTargetSensor : LocalTargetSensorBase
    {
        public GameObject target;

        public override void Created()
        {
        }

        public override void Update()
        {
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            var random = this.GetRandomPosition(agent);

            return new PositionTarget(random);
        }

        private Vector3 GetRandomPosition(IMonoAgent agent)
        {
            int count = 0;

            while (count < 5)
            {
                Vector2 random = Random.insideUnitCircle * 5;
                Vector3 position = agent.transform.position + new Vector3(random.x, 0, random.y);

                position = target.transform.position;
                count++;
            }

            return agent.transform.position;
        }
    }
}
