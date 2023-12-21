namespace Ability
{
    public abstract class AbilityPassiveDynamic : AbilityPassive<SoAbilityCooldown>
    {
        public override bool TryActivate()
        {
            return false;
        }

        public override void End()
        {
        }
    }
}
