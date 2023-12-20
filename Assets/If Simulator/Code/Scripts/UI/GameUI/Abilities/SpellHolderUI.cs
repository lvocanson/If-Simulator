using Ability;
using UnityEngine;

public class SpellHolderUI : MonoBehaviour
{
    [SerializeField] private LevelableAbilityUI _firstSpell;
    [SerializeField] private LevelableAbilityUI _secondSpell;
    

    public void UpdateFirstSpell(AbilityActive newSpell)
    {
        UpdateSpell(_firstSpell, newSpell);
    }
    
    public void UpdateSecondSpell(AbilityActive newSpell)
    {
        UpdateSpell(_secondSpell, newSpell);
    }
    
    private void UpdateSpell(LevelableAbilityUI spellUI, AbilityActive newSpell)
    {
        if (newSpell == null) return;
        
        spellUI.ChangeIcon(newSpell.RuntimeAbilitySo.Icon);
        spellUI.InitPassiveLevels(newSpell.RuntimeAbilitySo.MaxLevel);
        spellUI.LevelUpPassive(newSpell.CurrentLevel);
    }
}
