using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IT.WizardBattle.UI
{
    public class UIHealthbarView : MonoBehaviour
    {
        [SerializeField] private Image _fillBar;
        [SerializeField] private float _fillingDuration = 0.3f;

        private float _targetHealth = 1.0f;

        private Coroutine _animationCoroutine;



        /// <summary>
        /// Set health amount
        /// </summary>
        /// <param name="health">from 0 to 1</param>
        /// <param name="animated">animated</param>
        public void SetHealthAmount(float health, bool animated = true)
        {
            if (health < 0.0f || health > 1.0f)
                Debug.LogError($"[HEALTHBAR] Health is {health}, something gone wrong!");


            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
                _animationCoroutine = null;
            }

            _targetHealth = Mathf.Clamp01(health);

            if (!animated)
            {
                SetValue(health);
                return;
            }

            _animationCoroutine = StartCoroutine(FillHealthAnimated());
            
        }



        private IEnumerator FillHealthAnimated()
        {
            float speed = Mathf.Abs(_fillBar.fillAmount - _targetHealth) / _fillingDuration;
            float minValue = _fillBar.fillAmount;
            float maxValue = _targetHealth;

            if (_targetHealth < _fillBar.fillAmount)
            {
                speed *= -1.0f;
                minValue = _targetHealth;
                maxValue = _fillBar.fillAmount;
            }
            
            while (_fillBar.fillAmount != _targetHealth)
            {
                float newValue = _fillBar.fillAmount + speed * Time.deltaTime;
                _fillBar.fillAmount = Mathf.Clamp(newValue, minValue, maxValue);
                yield return null;
            }
            
        }

        private void SetValue(float value)
        {
            _fillBar.fillAmount = value;
        }

    }
}
