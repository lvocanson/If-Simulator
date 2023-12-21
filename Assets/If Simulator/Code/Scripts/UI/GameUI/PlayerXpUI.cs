using UnityEngine;
using UnityEngine.UI;

namespace If_Simulator.Code.Scripts.UI.GameUI
{
    public class PlayerXpUI : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _updateSpeed = 1f;
        
        public void UpdateValue(float value, float max, float level)
        {
            Debug.Log($"UpdateValue: {value} / {max} - {level}");
            _slider.value = value;
            _slider.maxValue = max;
        }
    }
}