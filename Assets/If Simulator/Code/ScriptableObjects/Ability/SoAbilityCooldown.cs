using UnityEngine;

namespace Ability
{
    [CreateAssetMenu(fileName = "NewActiveSo", menuName = "CustomSO/Active", order = 0)]
    public class SoAbilityCooldown : SoAbilityBase
    {
        [Header("Active Ability info")]
        public float AbilityCooldown;
        public float AbilityActiveCooldown;
    }
}