using NaughtyAttributes;
using UnityEngine;

namespace Ability
{
    public abstract class AbilityBase<T> : MonoBehaviour where T : SoAbilityBase
    {
        // Used for displaying the ability in the UI
        public ushort CurrentLevel { get; protected set; }

        public abstract bool TryActivate();

        public abstract void LevelUp();

        public abstract void End();

        [SerializeField, Expandable] protected T _abilitySo;
        
        public T RuntimeAbilitySo { get; private set; }
        
        protected virtual void Awake()
        {
            ResetAbility();
        }
        
        public void ResetAbility()
        {
            CurrentLevel = 1;
            RuntimeAbilitySo = Instantiate(_abilitySo);
        }
        
        public bool CompareAbility(SoAbilityBase otherAbility)
        {
            return _abilitySo.Name == otherAbility.Name;
        }
    }
}
