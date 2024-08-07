using IT.CoreLib.Interfaces;
using IT.WizardBattle.Game;
using System.Collections.Generic;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class EnemyAIService : IService, IUpdatable
    {
        private Transform _playerTransform => _playerService.PlayerTransform;

        private List<CharacterMoveController> _enemies = new();
        private PlayerService _playerService;
        

        public void OnInitialized(IContext context)
        {
            _playerService = context.GetService<PlayerService>();
        }

        public void Destroy()
        {
            _enemies.Clear();    
        }

        public void AddEnemy(CharacterMoveController enemy)
        {
            _enemies.Add(enemy);
        }

        public void Update(float dt)
        {
            foreach(CharacterMoveController moveController in _enemies)
            {
                //Dumb but still AI
                MoveAndRotateTowardsPlayer(moveController, dt);
            }
        }


        private void MoveAndRotateTowardsPlayer(CharacterMoveController moveController, float dt)
        {
            if (_playerTransform == null)
                return;

            moveController.RotateToward(_playerTransform.position, dt);
            moveController.Move(1.0f, dt);
        }
    }
}