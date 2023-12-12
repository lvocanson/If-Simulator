using UnityEngine;

namespace Ability
{
    public class SoAbilityBase : ScriptableObject
    {
        [Header("Ability info")]
        public string AbilityName;
        public string AbilityDescription;
        
        public Texture2D AbilityIcon;
        
        public ushort AbilityMaxLevel;
        
        public float AbilityDamage = 0f;
        public float AbilityRange = 1f;
    }
}