using System.Collections;
using UnityEngine;

namespace IT.WizardBattle.Game
{
    public class CharacterDamageEffect : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Color _damageColor = Color.red;

        private Color _originalColor;

        private const int EFFECT_BLINKING_COUNT = 3;
        private const float EFFECT_BLINKING_FREQUENCY = 5.0f;


        public void Initialize()
        {
            if (_sprite == null)
                _sprite = GetComponentInChildren<SpriteRenderer>();

            _originalColor = _sprite.color;
        }

        public void UpdateSpriteForced(GameObject visualObject)
        {
            _sprite = visualObject.GetComponent<SpriteRenderer>();
            _originalColor = _sprite.color;
        }

        public void PlayDamageEffect()
        {
            StopAllCoroutines();
            StartCoroutine(Blinking());
        }

        public void StopDamageEffect()
        {
            StopAllCoroutines();
            _sprite.color = _originalColor;
        }


        private IEnumerator Blinking()
        {
            float blinkDuration = 1.0f / EFFECT_BLINKING_FREQUENCY;
            float halfBlinkDuration = blinkDuration / 2.0f;

            for (int i = 0; i < EFFECT_BLINKING_COUNT; i++)
            {
                float timer = 0.0f;
                while (timer < blinkDuration)
                {
                    if (_sprite == null)
                        yield break;

                    if (timer < halfBlinkDuration)
                        _sprite.color = Color.Lerp(_sprite.color, _damageColor, timer / halfBlinkDuration);
                    else
                        _sprite.color = Color.Lerp(_sprite.color, _originalColor, (timer - halfBlinkDuration) / halfBlinkDuration);

                    timer += Time.deltaTime;

                    yield return null;
                }
                
            }

            _sprite.color = _originalColor;
        }
    }
}