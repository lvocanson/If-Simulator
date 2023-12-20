using Ability;
using UnityEngine;

public class SpellHolderUI : MonoBehaviour
{
    [SerializeField] private LevelableAbilityUI _firstSpell;
    [SerializeField] private LevelableAbilityUI _secondSpell;
    

    public void UpdateFirstSpell(SoAbilityBase newSpell)
    {
        UpdateSpell(_firstSpell, newSpell);
    }
    
    public void UpdateSecondSpell(SoAbilityBase newSpell)
    {
        UpdateSpell(_secondSpell, newSpell);
    }
    
    private void UpdateSpell(LevelableAbilityUI spellUI, SoAbilityBase newSpell)
    {
        // test
        if (newSpell == null)
        {
            spellUI.ChangeIcon(null);
            spellUI.InitPassiveLevels(3);
            spellUI.LevelUpPassive(1);
        }
        
        //TODO : Change the icon of the spell
        //TODO : Cooldown of the spell
        //TODO : Change level of the spell
        
        //spellUI.ChangeIcon(newSpell.Icon);
    }
}
