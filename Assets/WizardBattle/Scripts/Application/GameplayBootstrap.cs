using IT.CoreLib.Application;
using IT.WizardBattle.Services;
using UnityEngine;

namespace IT.WizardBattle.Application
{
    public class GameplayBootstrap : SceneBootstrap
    {
        [Header("Services prefabs")]
        [SerializeField] private PlayerService _playerServicePrefab;
        [SerializeField] private PlayerCastSpellsService _playerCastSpellsServicePrefab;
        [SerializeField] private EnemySpawnerService _enemySpawnerServicePrefab;

        private EnemySpawnerService _enemySpawnerService;
        private PlayerService _playerService;
        
        protected override void InitializeServices()
        {
            AddService<PlayerInputService>();
            AddService<SpellDataStorage>();
            AddService<EnemyDataStorage>();
            AddService<SpawnPointsService>();
            AddService<EnemyAIService>();
            _playerService = AddService<PlayerService>(_playerServicePrefab.gameObject);
            AddService<CameraService>();
            AddService<PlayerCastSpellsService>(_playerCastSpellsServicePrefab.gameObject);
            AddService<DamageService>();
            AddService<VFXService>();

            _enemySpawnerService = AddService<EnemySpawnerService>(_enemySpawnerServicePrefab.gameObject);
        }

        protected override void InitializeScene()
        {
            _enemySpawnerService.StartSpawning();
            _playerService.OnPlayerDied += OnPlayerDied;

        }

        private void OnPlayerDied()
        {
            SetPaused(true);
            SceneUI.ShowWindow<UIGameOverWindow>();
        }

        protected override void OnDestroy()
        {
            if (_playerService != null)
                _playerService.OnPlayerDied -= OnPlayerDied;

            base.OnDestroy();
        }
    }
}
