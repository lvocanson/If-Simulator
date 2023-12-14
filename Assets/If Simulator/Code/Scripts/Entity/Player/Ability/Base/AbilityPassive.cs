using UnityEngine;

namespace Ability
{
    public abstract class AbilityPassive<T> : AbilityBase<T> where T : SoAbilityBase
    {
        protected abstract void OnEffectStart();
        
        protected abstract void OnEffectEnd();
        
        public sealed override void LevelUp() => CurrentLevel = (ushort) Mathf.Clamp(CurrentLevel + 1, 0, _abilitySo.MaxLevel);
    }
}