using UnityEngine;

namespace IT.WizardBattle.Game
{
    public class CharacterMoveController : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

        public void SetSpeed(float movementSpeed, float rotationSpeed)
        {
            _movementSpeed = movementSpeed;
            _rotationSpeed = rotationSpeed;
        }

        /// <summary>
        /// Move character, tramsform.up is forward
        /// </summary>
        /// <param name="direction">1 - forward, -1 - backward</param>
        public void Move(float direction, float deltaTime)
        {
            transform.position += transform.up * direction * _movementSpeed * deltaTime;
        }

        /// <summary>
        /// Rotate character
        /// </summary>
        /// <param name="direction">1 - right, -1 - left</param>
        public void Rotate(float direction, float deltaTime)
        {
            transform.Rotate(0.0f, 0.0f, -direction * _rotationSpeed * deltaTime);
        }
    }
}
