using UnityEngine;
using UnityEngine.Serialization;

namespace Ability
{
    public abstract class Ability<T> : MonoBehaviour where T : SoAbilityBase
    {
        // Used for ScriptableObject instantiation, starts at "ScriptableObjects/"
        public string AbilitySoFilePath
        {
            set
            {
                _soFilePathFromRoot = value;
                _abilitySo = Resources.Load<T>($"{SO_FILE_ROOT}{_soFilePathFromRoot}");
            }
        }
        
        // Used for displaying the ability in the UI
        public ushort CurrentLevel { get; private set; }
        
        public abstract void TryActivate();
        
        public abstract void LevelUp();
        
        protected abstract void End();
        
        protected T _abilitySo;

        private string _soFilePathFromRoot;
        private const string SO_FILE_ROOT = "ScriptableObjects/";
    }
}