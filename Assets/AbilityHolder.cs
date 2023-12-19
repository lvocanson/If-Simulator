using System.Collections.Generic;
using Ability;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] protected GameObject _abilityPrefab;
    [SerializeField] protected LayoutGroup _layoutGroup;

    protected Dictionary<SoAbilityBase, AbilityIconUI> _currentAbilities;
    protected int _currentAbilityCount = 0;
    
    
    public void AddAbility(SoAbilityBase ability)
    {
        AbilityIconUI obj = Instantiate(_abilityPrefab, _layoutGroup.transform).GetComponent<AbilityIconUI>();
        _currentAbilities.Add(ability, obj);
        
        _currentAbilityCount++;
    }
    
    public void RemoveAbility(SoAbilityBase ability)
    {
        Destroy(_currentAbilities[ability]);
        _currentAbilities.Remove(ability);
        
        _currentAbilityCount--;
    }
}
