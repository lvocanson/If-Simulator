using Ability;
using UnityEngine;

public class SpellHolderUI : MonoBehaviour
{
    [SerializeField] private LevelableAbilityUI _firstSpell;
    [SerializeField] private LevelableAbilityUI _secondSpell;

    public LevelableAbilityUI FirstSpell;
    public LevelableAbilityUI SecondSpell;

    public void UpdateFirstSpell(AbilityActive newSpell)
    {
        UpdateSpell(_firstSpell, newSpell);
        UpdateFirstSpellCooldown(0, 0);
    }
    
    public void UpdateSecondSpell(AbilityActive newSpell)
    {
        UpdateSpell(_secondSpell, newSpell);
        UpdateSecondSpellCooldown(0, 0);
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

        spellUI.ChangeIcon(newSpell.RuntimeAbilitySo.Icon);
        spellUI.InitStars(newSpell.RuntimeAbilitySo.MaxLevel);
        spellUI.EnableStars(newSpell.CurrentLevel);
    }
}
