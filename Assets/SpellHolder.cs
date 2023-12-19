using System.Collections;
using System.Collections.Generic;
using Ability;
using UnityEngine;

public class SpellHolder : AbilityHolder
{
    public void UpdateFirstSpell(SoAbilityBase spell)
    {
        UpdateSpellIcon(0, spell);
    }
    
    public void UpdateSecondSpell(SoAbilityBase spell)
    {
        UpdateSpellIcon(1, spell);
    }
    
    private void UpdateSpellIcon(int index, SoAbilityBase spell)
    {
        
    }
}
