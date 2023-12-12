using UnityEngine;

namespace Ability.List
{
    public class AbilityPrimaryShoot : AbilityActive
    {
        protected override void OnEffectStart()
        {
            Debug.Log("AbilityPrimaryShoot: OnEffectStart");
        }

        protected override void OnEffectUpdate()
        {
            Debug.Log("AbilityPrimaryShoot: OnEffectUpdate");
        }

        protected override void OnEffectEnd()
        {
            Debug.Log("AbilityPrimaryShoot: OnEffectEnd");
        }
    }
}