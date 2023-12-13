using UnityEngine;
using UnityEngine.Serialization;

namespace Ability
{
    [CreateAssetMenu(fileName = "NewActiveSo", menuName = "Scriptable Objects/Active Ability", order = 0)]
    public class SoAbilityCooldown : SoAbilityBase
    {
        public float AbilityCooldown => _abilityCooldown;
        public float AbilityActiveCooldown => _abilityActiveCooldown;
        
        [Header("Ability cooldown")]
        [SerializeField] private float _abilityCooldown;
        [SerializeField] private float _abilityActiveCooldown;
    }
}