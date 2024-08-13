using IT.WizardBattle.Services;
using IT.WizardBattle.Interfaces;
using UnityEngine;

namespace IT.WizardBattle.Game.Player
{
    [RequireComponent(typeof(CharacterMoveController))]
    public class PlayerController : MonoBehaviour, IPlayerInstance
    {
        public GameObject GameObject => gameObject;
        public Transform ShootingPoint => _shootingPoint;


        [SerializeField] private Transform _shootingPoint;

        private CharacterMoveController _characterMoveController;
        private CharacterDamageEffect _characterDamageEffect;

        private PlayerService _playerService;

        private float _moveInput;
        private float _rotationInput;

        public void Initialize(PlayerService playerService, ICharacterData characterData)
        {
            _playerService = playerService;

            UpdateData(characterData);
        }

        public void UpdateData(ICharacterData characterData)
        {
            _characterMoveController.SetSpeed(characterData.Speed, characterData.RotationSpeed);
        }

        public void UpdateInput(float move, float rotation)
        {
            _moveInput = move;
            _rotationInput = rotation;
        }

        public void ReceiveDamage(float damage)
        {
            _characterDamageEffect.PlayDamageEffect();
            _playerService.AddDamage(damage);
        }

        public void Die()
        {
            _characterDamageEffect.StopDamageEffect();
            gameObject.SetActive(false);
        }


        private void Awake()
        {
            _characterMoveController = GetComponent<CharacterMoveController>();
            _characterDamageEffect = GetComponent<CharacterDamageEffect>();
            _characterDamageEffect.Initialize();
        }

        private void FixedUpdate()
        {
            _characterMoveController.Move(_moveInput, Time.fixedDeltaTime);
            _characterMoveController.Rotate(_rotationInput, Time.fixedDeltaTime);
        }
    }
}
