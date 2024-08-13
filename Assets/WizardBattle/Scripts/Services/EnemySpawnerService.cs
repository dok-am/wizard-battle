using IT.CoreLib.Extensions;
using IT.CoreLib.Interfaces;
using IT.WizardBattle.Data;
using IT.WizardBattle.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class EnemySpawnerService : IService, IUpdatable
    {
        public event Func<Vector2, bool> RequestIsPointVisible;
        public event Func<Vector2> RequestEnemySpawnPoint;
        public event Func<GameObject> RequestEnemyInstancePrefab;

        private const float SPAWNER_COOLDOWN_TIME = 1.0f;
        private const int MAX_ENEMIES_COUNT = 10;

        private EnemyStaticData[] _enemiesData;
        private EnemyAIService _enemyAIService;

        private bool _isSpawning;
        private float _spawnTimer;
        private List<IEnemyInstance> _enemiesPool = new();


        public void Initialize()
        {
            _isSpawning = false;
        }

        public void OnInitialized(IContext context)
        {
            _enemiesData = context.GetService<EnemyDataStorage>().GetAllModels();
            _enemyAIService = context.GetService<EnemyAIService>();
        }

        public void StartSpawning()
        {
            if (RequestIsPointVisible == null)
                throw new Exception("[SERVICE] Enemy spawn service isn't initialized: need RequestIsPointVisible binded");

            if (RequestEnemySpawnPoint == null)
                throw new Exception("[SERVICE] Enemy spawn service isn't initialized: need RequestEnemySpawnPoint binded");

            if (RequestEnemyInstancePrefab == null)
                throw new Exception("[SERVICE] Enemy spawn service isn't initialized: need RequestEnemyInstancePrefab binded");

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
            if (!IsEnemyAvailable())
                return;

            EnemyStaticData enemyData = _enemiesData.GetRandomItem();
            
            IEnemyInstance instance = GetEnemyFromPool(enemyData.Id);
            if (instance == null) 
            {
                instance = CreateNewEnemy();
            }

            instance.SetupEnemy(enemyData);
            instance.Spawn(GetRandomSpawnPointOutOfView());
            _enemyAIService.AddEnemy(instance.MoveController);
        }
        
        private IEnemyInstance GetEnemyFromPool(string typeId)
        {
            IEnemyInstance candidateEnemy = null;
            foreach (IEnemyInstance enemy in _enemiesPool)
            {
                if (enemy.Enabled)
                    continue;
                
                candidateEnemy = enemy;

                if (enemy.TypeId.Equals(typeId))
                    return enemy;
            }

            return candidateEnemy;
        }

        //TODO: Not really performant method, should be optimised
        private bool IsEnemyAvailable()
        {
            int activeEnemies = 0;
            foreach (IEnemyInstance enemy in _enemiesPool)
            {
                if (enemy.Enabled)
                    activeEnemies++;

                if (activeEnemies >= MAX_ENEMIES_COUNT)
                    return false;
            }

            return true;
        }

        private IEnemyInstance CreateNewEnemy()
        {
            IEnemyInstance instance = GameObject.Instantiate(RequestEnemyInstancePrefab()).GetComponent<IEnemyInstance>();
            if (instance == null)
                throw new Exception("[ENEMY] Can't spawn enemy: basic prefab is incorrect!");

            _enemiesPool.Add(instance);

            return instance;
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