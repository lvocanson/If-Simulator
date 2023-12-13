using UnityEngine;
using UnityEngine.Serialization;

namespace Ability
{
    public class SoAbilityBase : ScriptableObject
    {
        public string AbilityName => _abilityName;

        public string AbilityDescription => _abilityDescription;

        public Texture2D AbilityIcon => _abilityIcon;

        public ushort AbilityMaxLevel => _abilityMaxLevel;

        public float AbilityDamage => _abilityDamage;

        public float AbilityRange => _abilityRange;
        
        public bool IsHoldable => _isHoldable;

        [Header("Ability info")] 
        [SerializeField] private string _abilityName;
        [SerializeField] private string _abilityDescription;
        [SerializeField] private Texture2D _abilityIcon;
        [SerializeField] private ushort _abilityMaxLevel;
        [SerializeField] private float _abilityDamage;
        [SerializeField] private float _abilityRange;
        [SerializeField] private bool _isHoldable;
    }
}