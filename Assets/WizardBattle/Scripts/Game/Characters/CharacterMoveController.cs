using UnityEngine;

namespace IT.WizardBattle.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMoveController : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

        [SerializeField] private float _moveInertion = 5.0f;
        [SerializeField] private float _rotateInertion = 5.0f;

        [SerializeField] private Animator _animator;

        private Rigidbody2D _rigidbody;

        private float _moveValue;
        private float _rotationValue;

        private const string ANIMATOR_WALK_KEY = "Walk";

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

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
            _moveValue = Mathf.Lerp(_moveValue, direction * _movementSpeed, _moveInertion * deltaTime);
            _rigidbody.MovePosition(transform.position + transform.up * _moveValue * deltaTime);
        }

        /// <summary>
        /// Rotate character
        /// </summary>
        /// <param name="direction">1 - right, -1 - left</param>
        public void Rotate(float direction, float deltaTime)
        {
            _rotationValue = Mathf.Lerp(_rotationValue, direction * _rotationSpeed, _rotateInertion * deltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation - _rotationValue * deltaTime);
        }

        public void RotateToward(Vector2 target, float deltaTime)
        {
            Vector2 direction = target - (Vector2)transform.position;
            float angle = Vector2.SignedAngle(transform.up, direction);

            _rigidbody.MoveRotation(_rigidbody.rotation + angle * _rotationSpeed * deltaTime);
        }



        private void Update()
        {
            if (_animator != null) 
                _animator.SetBool(ANIMATOR_WALK_KEY, Mathf.Abs(_moveValue) > _movementSpeed/4.0f);
        }
    }
}
