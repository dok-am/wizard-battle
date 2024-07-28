using IT.WizardBattle.Services;
using IT.WizardBattle.Interfaces;
using UnityEngine;
using IT.WizardBattle.Assets.WizardBattle.Scripts.Game.Characters;

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
        private PlayerInputService _playerInputService;


        private void Awake()
        {
            _characterMoveController = GetComponent<CharacterMoveController>();
            _characterDamageEffect = GetComponent<CharacterDamageEffect>();
            _characterDamageEffect.Initialize();
        }

        public void Initialize(PlayerService playerService, PlayerInputService playerInputService, ICharacterData characterData)
        {
            _playerService = playerService;
            _playerInputService = playerInputService;
            _playerInputService.OnNextSpellPressed += OnNextSpellPressed;
            _playerInputService.OnPreviousSpellPressed += OnPreviousSpellPressed;

            UpdateData(characterData);
        }

        public void Deinitialize()
        {
            _playerInputService.OnNextSpellPressed -= OnNextSpellPressed;
            _playerInputService.OnPreviousSpellPressed -= OnPreviousSpellPressed;
        }

        public void UpdateData(ICharacterData characterData)
        {
            _characterMoveController.SetSpeed(characterData.Speed, characterData.RotationSpeed);
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



        private void OnNextSpellPressed()
        {
            _playerService.SelectNextSpell();
        }

        private void OnPreviousSpellPressed()
        {
            _playerService.SelectPreviousSpell();
        }

        private void FixedUpdate()
        {
            _characterMoveController.Move(_playerInputService.MoveValue, Time.fixedDeltaTime);
            _characterMoveController.Rotate(_playerInputService.RotateValue, Time.fixedDeltaTime);
        }

        
    }
}
