using IT.WizardBattle.Game;
using UnityEngine;

namespace IT.WizardBattle.Player
{
    [RequireComponent(typeof(CharacterMoveController))]
    [RequireComponent(typeof(PlayerInputController))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerInputController _playerInputController;
        private CharacterMoveController _characterMoveController;

        private void Awake()
        {
            _playerInputController = GetComponent<PlayerInputController>();
            _characterMoveController = GetComponent<CharacterMoveController>();

        }

        private void Update()
        {
            _characterMoveController.Move(_playerInputController.MoveValue, Time.deltaTime);
            _characterMoveController.Rotate(_playerInputController.RotateValue, Time.deltaTime);
        }
    }
}
