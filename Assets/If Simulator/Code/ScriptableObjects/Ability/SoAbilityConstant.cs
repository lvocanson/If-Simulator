using UnityEngine;
using UnityEngine.Serialization;

namespace Ability
{
    [CreateAssetMenu(fileName = "NewConstantSo", menuName = "Scriptable Objects/Constant Ability", order = 0)]
    public class SoAbilityConstant : SoAbilityBase
    {
        public bool _isActivated;
    }
}