namespace Ability
{
    public abstract class AbilityPassiveConstant : AbilityPassive<SoAbilityConstant>
    {
        public sealed override void TryActivate()
        {
            if (RuntimeAbilitySo._isActivated) return;

            OnEffectStart();
            RuntimeAbilitySo._isActivated = true;
        }

        public override void End()
        {
            OnEffectEnd();
        }
    }
}
