using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IT.WizardBattle
{
    public class PlayerInputController : MonoBehaviour
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

        private void Awake()
        {
            _moveAction = InputSystem.actions.FindAction("Move");
            _rotateAction = InputSystem.actions.FindAction("Rotate");
            _shootAction = InputSystem.actions.FindAction("Shoot");
            _nextSpellAction = InputSystem.actions.FindAction("Next");
            _previousSpellAction = InputSystem.actions.FindAction("Previous");

            _shootAction.started += _ => { OnShootPressed?.Invoke(); };
            _nextSpellAction.started += _ => { OnNextSpellPressed?.Invoke(); };
            _previousSpellAction.started += _ => { OnPreviousSpellPressed?.Invoke(); };

        }

        private void Update()
        {
            MoveValue = _moveAction.IsPressed() ? _moveAction.ReadValue<float>() : 0.0f;
            RotateValue = _rotateAction.IsPressed() ? _rotateAction.ReadValue<float>() : 0.0f;
        }
    }
}
