using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using IfSimulator.GOAP.Config;
using UnityEngine;

namespace IfSimulator.GOAP.Actions
{
    public class HealAction : ActionBase<HealData>, IInjectable
    {
        private HealConfigSO HealConfig;
        private DamageabaleEntity DamageabaleEntity;
        public override void Created()
        {
        }
        public override void Start(IMonoAgent agent, HealData data)
        {
            data.Timer = HealConfig.HealDelay;
        }
        public override ActionRunState Perform(IMonoAgent agent, HealData data, ActionContext context)
        {
            data.Timer -= context.DeltaTime;

            bool shouldHeal = data.Target != null && Vector3.Distance(data.Target.Position, agent.transform.position) <= HealConfig.HealRadius;

            if (shouldHeal)
            {
                agent.transform.up = data.Target.Position - agent.transform.position;
                DamageabaleEntity.Heal(HealConfig.HealAmount);
                Debug.Log(HealConfig.HealAmount);
                Debug.Log(DamageabaleEntity.CurrentHealth);
            }

            return data.Timer > 0 ? ActionRunState.Continue : ActionRunState.Stop;
        }

        public override void End(IMonoAgent agent, HealData data)
        {
        }

        public void Inject(DependencyInjector Injector)
        {
            HealConfig = Injector.HealConfig;
        }
    }
}
