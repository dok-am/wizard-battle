using IT.CoreLib.Extensions;
using IT.CoreLib.Interfaces;
using IT.WizardBattle.Data;
using System;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class EnemySpawnerService : IService, IUpdatable
    {
        public event Func<Vector2, bool> RequestIsPointVisible;
        public event Func<Vector2> RequestEnemySpawnPoint;
        public event Action<EnemyStaticConfig, Vector2> RequestSpawnEnemy;

        private const float SPAWNER_COOLDOWN_TIME = 1.0f;
        

        private EnemyStaticConfig[] _enemiesConfigs;

        private bool _isSpawning;
        private float _spawnTimer;


        public void Initialize()
        {
            _isSpawning = false;
        }

        public void OnInitialized(IContext context)
        {
            _enemiesConfigs = context.GetService<EnemyConfigStorage>().GetAllConfigs();
        }

        public void StartSpawning()
        {
            if (RequestIsPointVisible == null)
                throw new Exception("[SERVICE] Enemy spawn service isn't initialized: need RequestIsPointVisible binded");

            if (RequestEnemySpawnPoint == null)
                throw new Exception("[SERVICE] Enemy spawn service isn't initialized: need RequestEnemySpawnPoint binded");

            if (RequestSpawnEnemy == null)
                throw new Exception("[SERVICE] Enemy spawn service isn't initialized: need RequestSpawnEnemy binded");

            _isSpawning = true;
            _spawnTimer = 0.0f;
        }


        public void Update(float dt)
        {
            if (!_isSpawning)
                return;

            _spawnTimer += dt;
            if (_spawnTimer >= SPAWNER_COOLDOWN_TIME)
            {
                SpawnRandomEnemy();
                _spawnTimer = 0.0f;
            }
        }

        private void SpawnRandomEnemy()
        {
            EnemyStaticConfig enemyData = _enemiesConfigs.GetRandomItem();
            RequestSpawnEnemy(enemyData, GetRandomSpawnPointOutOfView());
        }
                

        private Vector2 GetRandomSpawnPointOutOfView()
        {
            Vector2 spawnPoint = RequestEnemySpawnPoint();

            if (RequestIsPointVisible.Invoke(spawnPoint))
                return GetRandomSpawnPointOutOfView();

            return spawnPoint;
        }

    }
}