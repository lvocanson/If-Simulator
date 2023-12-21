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
    
    public void OnFirstSpellActivated()
    {
        _firstSpell.OnUsed();
    }
    
    public void OnSecondSpellActivated()
    {
        _secondSpell.OnUsed();
    }
    
    public void UpdateFirstSpellCooldown(float newCooldown, float maxCooldown)
    {
        _firstSpell.UpdateCooldown(newCooldown, maxCooldown);
    }
    
    public void UpdateSecondSpellCooldown(float newCooldown, float maxCooldown)
    {
        _secondSpell.UpdateCooldown(newCooldown, maxCooldown);
    }
    
    private void UpdateSpell(LevelableAbilityUI spellUI, AbilityActive newSpell)
    {
        if (newSpell == null) return;

        Debug.Log("Updating spell: " + newSpell.RuntimeAbilitySo.Name);
        spellUI.ChangeIcon(newSpell.RuntimeAbilitySo.Icon);
        spellUI.InitStars(newSpell.RuntimeAbilitySo.MaxLevel);
        spellUI.EnableStars(newSpell.CurrentLevel);
    }
}
