using IT.WizardBattle.Data;
using IT.WizardBattle.Game;
using IT.WizardBattle.Services;
using UnityEngine;

namespace IT.WizardBattle.Player
{
    [RequireComponent(typeof(CharacterMoveController))]
    public class PlayerController : MonoBehaviour
    {
        public CharacterMoveController MoveController => _characterMoveController;
        private CharacterMoveController _characterMoveController;

        private PlayerService _playerService;
        private PlayerInputService _playerInputService;


        private void Awake()
        {
            _characterMoveController = GetComponent<CharacterMoveController>();
        }

        public void Initialize(PlayerService playerService, PlayerInputService playerInputService, ICharacterData characterData)
        {
            _playerService = playerService;
            _playerInputService = playerInputService;
            _playerInputService.OnNextSpellPressed += OnNextSpellPressed;
            _playerInputService.OnPreviousSpellPressed += OnPreviousSpellPressed;

            UpdateData(characterData);
        }

        public void UpdateData(ICharacterData characterData)
        {
            _characterMoveController.SetSpeed(characterData.Speed, characterData.RotationSpeed);
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
