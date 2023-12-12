namespace Ability
{
    public abstract class AbilityPassiveConstant : AbilityPassive<SoAbilityConstant>
    {
        public sealed override void TryActivate()
        {
            if (_abilitySo.IsActivated) return;

            OnEffectStart();
            _abilitySo.IsActivated = true;
        }

        protected override void End()
        {
            OnEffectEnd();
        }
    }
}