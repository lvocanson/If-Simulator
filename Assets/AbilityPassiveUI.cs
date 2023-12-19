using System.Collections;
using System.Collections.Generic;
using Ability;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPassiveUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private LayoutGroup _levelStarLayoutGroup;
    
    public Image Icon => _icon;
    public LayoutGroup LevelStarLayoutGroup => _levelStarLayoutGroup;
    
    
    public void InitPassive(SoAbilityBase passive)
    {
        //_icon.sprite = passive.Icon;
        
        
    }
    
    public void LevelUpPassive(int level)
    {
        
    } 
}
