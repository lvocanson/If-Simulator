using UnityEngine;

namespace Ability
{
    public class AbilityInvisibility : AbilityActive
    {
        [SerializeField] private CircleCollider2D _playerTrigger;
        [SerializeField] private SpriteRenderer _playerSprite;
        
        [SerializeField] private Color _invisibilityColor;
        private Color _defaultColor;
        
        protected override void OnEffectStart()
        {
            _playerTrigger.enabled = false;
            _defaultColor = _playerSprite.color;
            _playerSprite.color = _invisibilityColor;
        }

        protected override void OnEffectUpdate()
        {
            //TODO : exit invisibility on attack or hit
        }

        protected override void OnEffectEnd()
        {
            _playerTrigger.enabled = true;
            _playerSprite.color = _defaultColor;
        }
    }
}