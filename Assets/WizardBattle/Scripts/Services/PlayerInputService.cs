using System;
using IT.CoreLib.Interfaces;
using UnityEngine.InputSystem;

namespace IT.WizardBattle.Services
{
    public class PlayerInputService : IService, IUpdatable
    {
        public event Action OnShootPressed;
        public event Action OnNextSpellPressed;
        public event Action OnPreviousSpellPressed;

        public float MoveValue { get; private set; }
        public float RotateValue { get; private set; }      


        private InputAction _moveAction;
        private InputAction _rotateAction;
        private InputAction _shootAction;
        private InputAction _nextSpellAction;
        private InputAction _previousSpellAction;

        private bool _isPaused;


        public void Initialize()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
            _rotateAction = InputSystem.actions.FindAction("Rotate");
            _shootAction = InputSystem.actions.FindAction("Shoot");
            _nextSpellAction = InputSystem.actions.FindAction("Next");
            _previousSpellAction = InputSystem.actions.FindAction("Previous");

            _shootAction.started += ShootPressed;
            _nextSpellAction.started += NextSpellPressed;
            _previousSpellAction.started += PreviousSpellPressed;
        }

        public void OnPaused(bool paused)
        {
            _isPaused = paused;
        }

        public void Destroy()
        {
            _shootAction.started -= ShootPressed;
            _nextSpellAction.started -= NextSpellPressed;
            _previousSpellAction.started -= PreviousSpellPressed;
        }

        public void Update(float dt)
        {
            if (_isPaused)
            {
                MoveValue = 0.0f;
                RotateValue = 0.0f;
                return;
            }

            MoveValue = _moveAction.IsPressed() ? _moveAction.ReadValue<float>() : 0.0f;
            RotateValue = _rotateAction.IsPressed() ? _rotateAction.ReadValue<float>() : 0.0f;
        }


        private void ShootPressed(InputAction.CallbackContext context)
        {
            if (_isPaused)
                return;

            OnShootPressed?.Invoke();
        }

        private void NextSpellPressed(InputAction.CallbackContext context)
        {
            if (_isPaused)
                return;

            OnNextSpellPressed?.Invoke();
        }

        private void PreviousSpellPressed(InputAction.CallbackContext context)
        {
            if (_isPaused)
                return;

            OnPreviousSpellPressed?.Invoke();
        }
    }
}
