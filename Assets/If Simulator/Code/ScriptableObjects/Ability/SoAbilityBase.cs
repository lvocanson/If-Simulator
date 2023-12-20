using UnityEngine;

namespace Ability
{
    public class SoAbilityBase : ScriptableObject
    {
        public string Name => _name;

        public string Description => _description;

        public Sprite Icon => _icon;

        public ushort MaxLevel => _maxLevel;
        
        public float Range => _range;
        
        public float Value => _value;
        
        public virtual void LevelUp()
        {
            _value += _valuePerLevel;
            _range += _rangePerLevel;
        }

        [Header("Ability info")] 
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ushort _maxLevel;
        [SerializeField] private float _range;
        [SerializeField, Tooltip("The value used by this spell (i.e : damage, stat increase, speed, ...)")] private float _value;
        
        [Header("Level up properties")]
        [SerializeField] private float _valuePerLevel;
        [SerializeField] private float _rangePerLevel;
    }
}