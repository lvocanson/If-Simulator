using UnityEngine;
using UnityEngine.Serialization;

namespace Ability
{
    public abstract class Ability : MonoBehaviour 
    {
        // Used for displaying the ability in the UI
        public ushort CurrentLevel { get; private set; }

        public abstract void TryActivate();
        protected abstract void End();
        
        public abstract void LevelUp();
    }
}