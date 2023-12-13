namespace Ability
{
    public abstract class AbilityPassiveConstant : AbilityPassive<SoAbilityConstant>
    {
        public sealed override void TryActivate()
        {
            if (_abilitySo._isActivated) return;

            OnEffectStart();
            _abilitySo._isActivated = true;
        }

        public override void End()
        {
            OnEffectEnd();
        }
    }
}