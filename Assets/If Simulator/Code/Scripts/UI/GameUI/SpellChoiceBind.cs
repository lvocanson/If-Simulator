using System.Collections.Generic;
using Ability;
using UnityEngine;

namespace UI
{
    public class SpellChoiceBind : MonoBehaviour
    {
        [SerializeField] private PlayerUIManager _playerUiManager;
        [SerializeField] private GameObject _popup;

        [SerializeField] private List<SpellChoicePopupCard> _cards;
        private SoAbilityBase _context;

        public void Init(SoAbilityBase so)
        {
            _context = so;
            
            _popup.SetActive(true);
            
            string firstSpellName = _playerUiManager.CurrentPlayerSo.Player.PlayerAttackManager.GetFirstSpell()?.Name;
            string secondSpellName = _playerUiManager.CurrentPlayerSo.Player.PlayerAttackManager.GetSecondSpell()?.Name;

            if (_context.Name == secondSpellName)
            {
                _cards[0].gameObject.SetActive(false);
            }
            else
            {
                _cards[0].Init(_playerUiManager.CurrentPlayerSo.Player.PlayerAttackManager.GetFirstSpell(), _context);
                _cards[0].OnCardClicked += SelectFirstSpell;
            }
            
            if (_context.Name == firstSpellName)
            {
                _cards[1].gameObject.SetActive(false);
            }
            else
            {
                _cards[1].Init(_playerUiManager.CurrentPlayerSo.Player.PlayerAttackManager.GetSecondSpell(), _context);
                _cards[1].OnCardClicked += SelectSecondSpell;
            }
        }
        
        private void Update()
        {
            // TODO CHANGE THIS BECAUSE THIS IS SUPER UGLY
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Exit();
            }
        }

        private void SelectFirstSpell(SoAbilityBase so)
        {
            _playerUiManager.ChangeFirstSpell(so);
            _cards[0].OnCardClicked -= SelectFirstSpell;
            Exit();
        }
        
        private void SelectSecondSpell(SoAbilityBase so)
        {
            _playerUiManager.ChangeSecondSpell(so);
            _cards[1].OnCardClicked -= SelectSecondSpell;
            Exit();
        }

        private void Exit()
        {
            App.InputManager.SwitchMode(InputManager.InputMode.Gameplay);
            Cursor.visible = false;
            Time.timeScale = 1;
            _popup.SetActive(false);
        }
    }
}