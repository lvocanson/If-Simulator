using System;
using UnityEngine;

namespace Ability
{
    public class SecondaryFireExplosionBehavior : AbilityExplosionBehavior
    {
        private void Update()
        {
            OnUpdate();
        }

        public override void OnUpdate()
        {
            _timer += Time.fixedDeltaTime / _so.AbilityDuration;
            float power = _evolutionCurve.Evaluate(_timer);

            _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, 1 - power);
            transform.localScale = Vector3.one * (power * _maxSize);
            
            if (_timer >= 1)
            {
                Destroy(gameObject);
            }
        }

        protected override void HandleCollision(Collider2D other)
        {
            
        }
    }
}