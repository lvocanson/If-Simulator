using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes;
using CrashKonijn.Goap.Enums;
using CrashKonijn.Goap.Interfaces;
using IfSimulator.GOAP.Config;
using UnityEngine;

namespace IfSimulator.GOAP.Actions
{
    public class MeleeAction : ActionBase<AttackData>, IInjectable
    {

        private AttackConfigSO AttackConfig;
        private Enemy _enemy;
        private float cooldownTimer = 0f;

        public override void Created()
        {
            cooldownTimer = 2f;
        }

        public override void Start(IMonoAgent agent, AttackData data)
        {
            data.Timer = AttackConfig.AttackDelay;
            _enemy = (agent.CurrentActionData.Target as TransformTarget).Transform.GetComponent<Enemy>();
        }

        public override ActionRunState Perform(IMonoAgent agent, AttackData data, ActionContext context)
        {
            cooldownTimer -= context.DeltaTime;

            if (cooldownTimer <= 0)
            {
                data.Timer -= context.DeltaTime;

                bool shouldAttack = data.Target != null && Vector3.Distance(data.Target.Position, agent.transform.position) <= AttackConfig.MeleeAttackRadius;

                if (shouldAttack)
                {
                    Debug.Log("Attack");
                    agent.transform.up = data.Target.Position - agent.transform.position;
                    _enemy.Damage(AttackConfig.AttackDamage);

                    Debug.Log(_enemy.CurrentHealth);
                    cooldownTimer = AttackConfig.AttackDelay;
                    return ActionRunState.Continue;
                }
            }

            return ActionRunState.Stop;
        }

        public override void End(IMonoAgent agent, AttackData data)
        { 
        }

        public void Inject(DependencyInjector injector)
        {
            AttackConfig = injector.AttackConfig;
        }
    }
}

