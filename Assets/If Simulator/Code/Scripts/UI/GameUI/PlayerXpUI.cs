using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace If_Simulator.Code.Scripts.UI.GameUI
{
    public class PlayerXpUI : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private float _updateSpeed = 1f;
        
        public void UpdateValue(float value, float max, int level)
        {
            _slider.value = value;
            _slider.maxValue = max;
            _levelText.text = "lvl." + level.ToString();
        }
    }
}