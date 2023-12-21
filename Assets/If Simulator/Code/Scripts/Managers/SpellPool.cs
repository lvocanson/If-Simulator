using System.Collections.Generic;
using Ability;
using UnityEngine;

namespace Managers
{
    public class SpellPool : MonoBehaviour
    {
        [SerializeField, Tooltip("Base abilities such as primary fire, secondary fire")] 
        private List<SoAbilityBase> _baseAbilities;
        
        [SerializeField, Tooltip("All active spell available in game")] 
        private List<SoAbilityBase> _spells;
        
        [SerializeField, Tooltip("All passive abilities available in game")] 
        private List<SoAbilityBase> _passives;
        
        public SoAbilityBase GetRandomSpell()
        {
            // TODO : Add logic to get random passive or active spell
            return _spells[Random.Range(0, _spells.Count)];
        }
    }
}