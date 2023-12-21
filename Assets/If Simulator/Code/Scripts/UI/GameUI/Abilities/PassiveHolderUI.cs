using System.Collections.Generic;
using Ability;
using UnityEngine;
using UnityEngine.UI;

public class PassiveHolderUI : MonoBehaviour
{
    [SerializeField] protected GameObject _abilityPrefab;
    [SerializeField] protected LayoutGroup _layoutGroup;
    
    
    private Dictionary<SoAbilityBase, LevelableAbilityUI> _currentAbilities = new();
    
    
    public void AddPassive(SoAbilityBase ability)
    {
        LevelableAbilityUI obj = Instantiate(_abilityPrefab, _layoutGroup.transform).GetComponent<LevelableAbilityUI>();
        _currentAbilities.Add(ability, obj);
        obj.InitStars(ability.MaxLevel);
    }
    
    public void RemovePassive(SoAbilityBase ability)
    {
        Destroy(_currentAbilities[ability]);
        _currentAbilities.Remove(ability);
    }
    
    public void LevelUpPassive(SoAbilityBase ability)
    {
        //_currentAbilities[ability].LevelUpPassive(ability.CurrentLevel);
    }
}
