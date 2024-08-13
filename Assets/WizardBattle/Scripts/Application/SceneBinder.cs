using IT.CoreLib.Application;
using IT.CoreLib.Interfaces;
using IT.WizardBattle.Game;
using IT.WizardBattle.Game.Player;
using IT.WizardBattle.Interfaces;
using IT.WizardBattle.Managers;
using IT.WizardBattle.Services;
using UnityEngine;

namespace IT.WizardBattle.Application
{
    public class SceneBinder : SceneBinderBase
    {
        [Header("Scene objects")]
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform[] _enemiesSpawnPoints;

        [Header("Prefabs")]
        [SerializeField] private PlayerController _playerPrefab;
        [SerializeField] private EnemyController _enemyInstancePrefab;
        [SerializeField] private SpellInstance _spellInstancePrefab;

        private EnemySpawnerService _enemySpawnerService;
        private PlayerCastSpellsService _playerCastSpellsService;

        private PlayerManager _playerManager;
        private CameraManager _cameraManager;
        private SpawnPointsManager _spawnPointsManager;
        private EnemySpawnManager _enemySpawnManager;

        public override void Bind(IContext context)
        {
            _enemySpawnerService = context.GetService<EnemySpawnerService>();
            _playerCastSpellsService = context.GetService<PlayerCastSpellsService>();

            _playerCastSpellsService.RequestSpellInstancePrefab += GetSpellPrefabGO;

            BindSpawnPoints(context);
            BindPlayer(context);
            BindCamera(context);
            BindEnemiesSpawner(context);
        }

        public override void Unbind(IContext context)
        {
            if (_enemySpawnerService != null)
            {
                _enemySpawnerService.RequestIsPointVisible -= _cameraManager.IsPointVisible;
                _enemySpawnerService.RequestEnemySpawnPoint -= _spawnPointsManager.GetRandomEnemySpawnPoint;
                _enemySpawnerService.RequestSpawnEnemy += _enemySpawnManager.SpawnEnemyIfPossible;
            }

            if (_playerCastSpellsService != null)
                _playerCastSpellsService.RequestSpellInstancePrefab -= GetSpellPrefabGO;

            _playerManager.OnPlayerSpawned -= OnPlayerSpawned;

            base.Unbind(context);
        }


        private void BindPlayer(IContext context)
        {
            _playerManager = new PlayerManager(_playerPrefab,
                context.GetService<PlayerService>(),
                context.GetService<PlayerInputService>(),
                _spawnPointsManager);

            AddManager(_playerManager);
        }

        private void BindCamera(IContext context)
        {
            _cameraManager = new CameraManager(_mainCamera);

            _playerManager.OnPlayerSpawned += OnPlayerSpawned;
            _enemySpawnerService.RequestIsPointVisible += _cameraManager.IsPointVisible;

            AddManager(_cameraManager);
        }

        private void BindSpawnPoints(IContext context)
        {
            _spawnPointsManager = new SpawnPointsManager(_playerSpawnPoint, _enemiesSpawnPoints);
            _enemySpawnerService.RequestEnemySpawnPoint += _spawnPointsManager.GetRandomEnemySpawnPoint;

            AddManager(_spawnPointsManager);
        }

        private void BindEnemiesSpawner(IContext context)
        {
            _enemySpawnManager = new(_enemyInstancePrefab, context.GetService<EnemyAIService>());
            _enemySpawnerService.RequestSpawnEnemy += _enemySpawnManager.SpawnEnemyIfPossible;
        }

        private GameObject GetSpellPrefabGO() => _spellInstancePrefab.gameObject;

        private void OnPlayerSpawned(IPlayerInstance playerInstance)
        {
            _cameraManager.SetTarget(playerInstance.GameObject.transform);
        }
    }
}