using UnityEngine;
using UnityEngine.Serialization;

namespace Ability
{
    [CreateAssetMenu(fileName = "NewActiveSo", menuName = "Scriptable Objects/Active Ability", order = 0)]
    public class SoAbilityCooldown : SoAbilityBase
    {
        public float Cooldown => _cooldown;
        public float ActiveCooldown => _activeCooldown;
        
        [Header("Ability cooldown")]
        [SerializeField] private float _cooldown;
        [SerializeField] private float _activeCooldown;
    }
}