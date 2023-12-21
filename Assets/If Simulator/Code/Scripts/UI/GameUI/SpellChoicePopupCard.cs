using System;
using Ability;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpellChoicePopupCard : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _spellType;
        
        public SoAbilityBase DisplayedSpellSo => _displayedSpellSo;
        private SoAbilityBase _displayedSpellSo;
        
        private SoAbilityBase _context;
        
        public event Action<SoAbilityBase> OnCardClicked;

        public void Init(SoAbilityBase displayed, SoAbilityBase context)
        {
            _displayedSpellSo = displayed;
            _context = context;
            
            _icon.sprite = _displayedSpellSo.Icon;
            _nameText.text = _displayedSpellSo.Name;
            _spellType.text = _displayedSpellSo.Type.ToString();
        }
        
        public void OnClick()
        {
            OnCardClicked?.Invoke(_context);
        }
    }
}