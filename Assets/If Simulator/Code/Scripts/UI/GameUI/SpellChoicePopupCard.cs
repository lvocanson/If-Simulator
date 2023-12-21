using System;
using Ability;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpellChoicePopupCard : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _spellType;
        
        private SoAbilityBase _soAbilityBase;
        
        public event Action<SoAbilityBase> OnCardClicked;

        public void Init(SoAbilityBase so)
        {
            _button.onClick.AddListener(OnClick);
            _soAbilityBase = so;
            
            _icon.sprite = _soAbilityBase.Icon;
            _nameText.text = _soAbilityBase.Name;
            _spellType.text = _soAbilityBase.Type.ToString();
        }
        
        public void OnClick()
        {
            OnCardClicked?.Invoke(_soAbilityBase);
        }
    }
}