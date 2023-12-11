using UnityEngine;

namespace Ability
{
    public class AbilityScriptableObject : ScriptableObject
    {
        public string AbilityName;
        public string AbilityDescription;
        
        public Sprite AbilityIcon;
        
        public ushort AbilityMaxLevel;
        
        public float AbilityDamage;
        
        public float AbilityCooldown;
        public float AbilityActiveCooldown;
    }
}