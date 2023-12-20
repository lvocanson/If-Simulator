using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using CrashKonijn.Goap.Resolver;
using CrashKonijn.Goap.Enums;

using IfSimulator.GOAP.Actions;
using IfSimulator.GOAP.Goals;
using IfSimulator.GOAP.WorldKeys;
using IfSimulator.GOAP.Targets;
using IfSimulator.GOAP.Sensors;

using UnityEngine;

namespace IfSimulator.GOAP.Factories
{
    [RequireComponent(typeof(DependencyInjector))]
    public class GoapSetConfigFactory : GoapSetFactoryBase
    {
        private DependencyInjector Injector;
        public override IGoapSetConfig Create()
        {
            Injector = GetComponent<DependencyInjector>();
            GoapSetBuilder builder = new("AllySet");

            BuildGoals(builder);
            BuildActions(builder);
            BuildSensors(builder);

            return builder.Build();
        }

        private void BuildGoals(GoapSetBuilder builder)
        {
            builder.AddGoal<WanderGoal>() 
                .AddCondition<IsWandering>(Comparison.GreaterThanOrEqual, 1);

            builder.AddGoal<HealAlly>()
                .AddCondition<PlayerHealth>(Comparison.SmallerThanOrEqual, 80);

        }

        private void BuildActions(GoapSetBuilder builder)
        {
            builder.AddAction<WanderAction>()
                .SetTarget<WanderTarget>()
                .AddEffect<IsWandering>(EffectType.Increase)
                .SetBaseCost(5)
                .SetInRange(10);

            builder.AddAction<HealAction>()
                .SetTarget<PlayerTarget>()
                .AddEffect<PlayerHealth>(EffectType.Decrease)
                .SetBaseCost(Injector.HealConfig.HealCost)
                .SetInRange(Injector.HealConfig.HealRadius);
        }

        private void BuildSensors(GoapSetBuilder builder)
        {
            builder.AddTargetSensor<WanderTargetSensor>()
                .SetTarget<WanderTarget>();

            builder.AddTargetSensor<PlayerTargetSensor>()
                .SetTarget<PlayerTarget>();
        }
    }
}
