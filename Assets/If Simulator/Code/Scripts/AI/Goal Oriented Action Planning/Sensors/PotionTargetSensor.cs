using CrashKonijn.Goap.Interfaces;
using CrashKonijn.Goap.Sensors;

namespace IfSimulator.GOAP.Sensors
{
    public class PotionTargetSensor : LocalTargetSensorBase
    {
        public override void Created()
        {
        }

        public override ITarget Sense(IMonoAgent agent, IComponentReference references)
        {
            return null;
        }

        public override void Update()
        {
        }
    }

}

