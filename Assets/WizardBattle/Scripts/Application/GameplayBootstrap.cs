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
        
        protected override void InitializeServices()
        {
            AddService<PlayerInputService>();
            AddService<SpellDataStorage>();
            AddService<EnemyDataStorage>();
            AddService<SpawnPointsService>();
            AddService<EnemyAIService>();
            AddService<PlayerService>(_playerServicePrefab.gameObject);
            AddService<CameraService>();
            AddService<PlayerCastSpellsService>(_playerCastSpellsServicePrefab.gameObject);

            _enemySpawnerService = AddService<EnemySpawnerService>(_enemySpawnerServicePrefab.gameObject);
        }

        protected override void InitializeScene()
        {
            _enemySpawnerService.StartSpawning();
        }
    }
}
