using System;
using System.Collections.Generic;
using Ability;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SpellChoicePopup : MonoBehaviour
    {
        [SerializeField] private PlayerUIManager _playerUIManager;
        [SerializeField] private GameObject _popup;
        [SerializeField] private List<SpellChoicePopupCard> _cards;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private int _numberOfCards = 3;
        
        [SerializeField] private SpellChoiceBind _spellChoiceBind;
        
        [SerializeField] private string _levelTextFormat = "New Level : {0}";

        private void OnEnable()
        {
            foreach (SpellChoicePopupCard card in _cards)
            {
                card.OnCardClicked += Exit;
            }
        }

        private void OnDisable()
        {
            foreach (SpellChoicePopupCard card in _cards)
            {
                card.OnCardClicked -= Exit;
            }
        }

        public void Init(int level)
        {
            App.InputManager.SwitchMode(InputManager.InputMode.UI);
            Cursor.visible = true;
            
            Time.timeScale = 0;
            
            _popup.SetActive(true);
            _levelText.text = string.Format(_levelTextFormat, level);
            GetRandomSpellsFromPool();
        }

        private void Update()
        {
            // TODO CHANGE THIS BECAUSE THIS IS SUPER UGLY
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Exit(null);
            }
        }

        private void GetRandomSpellsFromPool()
        {
            for (int i = 0; i < _numberOfCards; i++)
            {
                SoAbilityBase spell = LevelContext.Instance.SpellPool.GetRandomSpell();
                _cards[i].Init(spell, spell);
            }
        }
        
        private void Exit(SoAbilityBase so)
        {
            Debug.Log("Triggered exit");
            if (so == null)
            {
                InternalExit();
                return;
            }

            PlayerAttackManager playerAttackManager = _playerUIManager.CurrentPlayerSo.Player.PlayerAttackManager;
            if (playerAttackManager.GetFirstSpell() == null || playerAttackManager.GetFirstSpell()?.Name == so.Name)
            {
                _playerUIManager.ChangeFirstSpell(so);
                InternalExit();
            }
            else if (playerAttackManager.GetSecondSpell() == null)
            {
                _playerUIManager.ChangeSecondSpell(so);
                InternalExit();
            }
            else
            {
                _spellChoiceBind.Init(so);
                _popup.SetActive(false); 
            }
        }
        
        private void InternalExit()
        {
            App.InputManager.SwitchMode(InputManager.InputMode.Gameplay);
            Cursor.visible = false;
            Time.timeScale = 1;
            _popup.SetActive(false);
        }
    }
}