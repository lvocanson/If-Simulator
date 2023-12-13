using UnityEngine;

namespace Ability
{
    public abstract class AbilityBase<T> : MonoBehaviour where T : SoAbilityBase
    {
        // Used for displaying the ability in the UI
        public ushort CurrentLevel { get; protected set; }

        public abstract void TryActivate();

        public abstract void LevelUp();

        public abstract void End();

        [SerializeField] protected T _abilitySo;
    }
}