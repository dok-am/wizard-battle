using IT.CoreLib.Application;
using IT.WizardBattle.Services;
using UnityEngine;

namespace IT.WizardBattle.Application
{
    public class GameplaySceneContext : SceneContext
    {
        private EnemySpawnerService _enemySpawnerService;
        private PlayerService _playerService;
        
        protected override void InitializeServices()
        {
            AddService<PlayerInputService>();
            AddService<SpellDataStorage>();
            AddService<EnemyDataStorage>();
            AddService<EnemyAIService>();
            AddService<PlayerCastSpellsService>();
            AddService<DamageService>();
            AddService<VFXService>();

            _playerService = AddService<PlayerService>();
            _enemySpawnerService = AddService<EnemySpawnerService>();
        }

        protected override void InitializeScene()
        {
            _playerService.OnPlayerDied += OnPlayerDied;

            StartGame();
        }

        protected override void OnDestroy()
        {
            if (_playerService != null)
                _playerService.OnPlayerDied -= OnPlayerDied;

            base.OnDestroy();
        }


        private void StartGame()
        {
            _playerService.RespawnPlayer();
            _enemySpawnerService.StartSpawning();
        }

        private void OnPlayerDied()
        {
            SetPaused(true);
            SceneUI.ShowWindow<UIGameOverWindow>();
        }
    }
}
