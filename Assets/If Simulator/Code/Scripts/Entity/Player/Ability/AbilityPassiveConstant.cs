namespace Ability
{
    public abstract class AbilityPassiveConstant : AbilityPassive<SoAbilityConstant>
    {
        public sealed override bool TryActivate()
        {
            if (_abilitySo._isActivated) return false;

            OnEffectStart();
            _abilitySo._isActivated = true;

            return true;
        }

        public override void End()
        {
            OnEffectEnd();
        }
    }
}
