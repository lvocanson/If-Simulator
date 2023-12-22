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
        private Player Player;
        private float cooldownTimer = 0f;

        public override void Created()
        {
            cooldownTimer = 0.5f;
        }

        public override void Start(IMonoAgent agent, HealData data)
        {
            data.Timer = HealConfig.HealDelay;
            Player = LevelContext.Instance.LevelManager.SpawnedPlayer; // TODO refactor this    
        }

        public override ActionRunState Perform(IMonoAgent agent, HealData data, ActionContext context)
        {
            cooldownTimer -= context.DeltaTime;
            
            agent.transform.up = data.Target.Position - agent.transform.position;
            
            if (cooldownTimer <= 0)
            {
                data.Timer -= context.DeltaTime;

                bool shouldHeal = data.Target != null && Vector3.Distance(data.Target.Position, agent.transform.position) <= HealConfig.HealRadius;

                if (shouldHeal && Player.CurrentHealth <= HealConfig.HealThreshold)
                {
                    agent.transform.up = data.Target.Position - agent.transform.position;
                    Player.Heal(HealConfig.HealAmount, LevelContext.Instance.GameSettings.HealColor);
                    
                    cooldownTimer = HealConfig.HealDelay;

                    return Player.CurrentHealth >= HealConfig.HealThreshold ? ActionRunState.Stop : ActionRunState.Continue;
                }
            }

            return ActionRunState.Stop;
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
