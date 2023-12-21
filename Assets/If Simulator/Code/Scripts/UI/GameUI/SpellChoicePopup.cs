using System;
using System.Collections.Generic;
using Ability;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SpellChoicePopup : MonoBehaviour
    {
        public enum SpellType
        {
            FIRST,
            SECOND
        }
        
        [SerializeField] private PlayerUIManager _playerUIManager;
        
        [SerializeField] private GameObject _popup;
        [SerializeField] private List<SpellChoicePopupCard> _cards;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private int _numberOfCards = 3;
        
        [SerializeField] private string _levelTextFormat = "New Level : {0}";

        public void Init(int level)
        {
            App.InputManager.SwitchMode(InputManager.InputMode.UI);
            Cursor.visible = true;
            
            Time.timeScale = 0;
            
            _popup.SetActive(true);
            _levelText.text = string.Format(_levelTextFormat, level);
            GetRandomSpellsFromPool();
            
            foreach (SpellChoicePopupCard card in _cards)
            {
                card.OnCardClicked += Exit;
            }
        }

        private void GetRandomSpellsFromPool()
        {
            for (int i = 0; i < _numberOfCards; i++)
            {
                SoAbilityBase spell = LevelContext.Instance.SpellPool.GetRandomSpell();
                _cards[i].Init(spell);
            }
        }
        
        private void Exit(SoAbilityBase so)
        {
            App.InputManager.SwitchMode(InputManager.InputMode.Gameplay);
            Cursor.visible = false;
            Time.timeScale = 1;
            
            _playerUIManager.CurrentPlayerSo.Player.PlayerAttackManager.ChangeFirstSpell(so);
            
            _popup.SetActive(false);
        }
    }
}