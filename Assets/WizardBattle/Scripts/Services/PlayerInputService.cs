using System;
using IT.CoreLib.Interfaces;
using UnityEngine.InputSystem;

namespace IT.WizardBattle.Services
{
    public class PlayerInputService : IService, IUpdatable
    {
        public float MoveValue { get; private set; }
        public float RotateValue { get; private set; }

        public event Action OnShootPressed;
        public event Action OnNextSpellPressed;
        public event Action OnPreviousSpellPressed;


        private InputAction _moveAction;
        private InputAction _rotateAction;
        private InputAction _shootAction;
        private InputAction _nextSpellAction;
        private InputAction _previousSpellAction;


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

        public void OnInitialized(IBootstrap bootstrap)
        {

        }

        public void Destroy()
        {
            _shootAction.started -= ShootPressed;
            _nextSpellAction.started -= NextSpellPressed;
            _previousSpellAction.started -= PreviousSpellPressed;
        }

        public void Update(float dt)
        {
            MoveValue = _moveAction.IsPressed() ? _moveAction.ReadValue<float>() : 0.0f;
            RotateValue = _rotateAction.IsPressed() ? _rotateAction.ReadValue<float>() : 0.0f;
        }

        private void ShootPressed(InputAction.CallbackContext context)
        {
            OnShootPressed?.Invoke();
        }

        private void NextSpellPressed(InputAction.CallbackContext context)
        {
            OnNextSpellPressed?.Invoke();
        }

        private void PreviousSpellPressed(InputAction.CallbackContext context)
        {
            OnPreviousSpellPressed?.Invoke();
        }
    }
}
