using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class BackdropManager : MonoBehaviour
    {
        [SerializeField] private Image _backdrop;
        [SerializeField] private float _time = 1.5f;

        [SerializeField] private AnimationCurve _curve;

        public IEnumerator Activate()
        {
            _backdrop.gameObject.SetActive(true);

            var timer = 0f;
            while (timer <= 1f)
            {
                timer += Time.unscaledDeltaTime / _time;

                var newColor = _backdrop.color;
                newColor.a = _curve.Evaluate(timer);
                _backdrop.color = newColor;

                yield return null;
            }
        }

        public IEnumerator Release()
        {
            var timer = 0f;
            while (timer <= 1f)
            {
                timer += Time.unscaledDeltaTime / _time;

                var newColor = _backdrop.color;
                newColor.a = _curve.Evaluate(1 - timer);
                _backdrop.color = newColor;

                yield return null;
            }

            _backdrop.gameObject.SetActive(false);
        }

        public void SetActivePanel()
        {
            _backdrop.gameObject.SetActive(true);
        }
    }
}

