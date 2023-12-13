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

        public override void End()
        {
            OnEffectEnd();
        }
    }
}