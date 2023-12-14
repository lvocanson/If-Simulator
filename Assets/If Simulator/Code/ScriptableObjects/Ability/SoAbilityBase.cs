using UnityEngine;
using UnityEngine.Serialization;

namespace Ability
{
    public class SoAbilityBase : ScriptableObject
    {
        public string Name => _name;

        public string Description => _description;

        public Texture2D Icon => _icon;

        public ushort MaxLevel => _maxLevel;

        public float Damage => _damage;

        public float Range => _range;

        [Header("Ability info")] 
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Texture2D _icon;
        [SerializeField] private ushort _maxLevel;
        [SerializeField] private float _damage;
        [SerializeField] private float _range;
    }
}