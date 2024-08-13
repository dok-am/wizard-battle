using IT.CoreLib.Interfaces;
using IT.WizardBattle.Data;
using IT.WizardBattle.Game;
using IT.WizardBattle.Interfaces;
using IT.WizardBattle.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace IT.WizardBattle.Managers
{
    public class EnemySpawnManager : IManager
    {
        private const int MAX_ENEMIES_COUNT = 10;

        private EnemyController _enemyPrefab;
        private EnemyAIService _enemyAIService;

        private Dictionary<string, ObjectPool<IEnemyInstance>> _enemiesPool = new();
        private Transform _enemiesContainer;
        private int _spawnedEnemiesCount = 0;

        public EnemySpawnManager(EnemyController enemyPrefab, EnemyAIService aiService)
        {
            _enemyPrefab = enemyPrefab;
            _enemyAIService = aiService;
            _enemiesContainer = new GameObject("ENEMIES_POOL").transform;
        }

        public void SpawnEnemyIfPossible(EnemyStaticConfig config, Vector2 position)
        {
            if (_spawnedEnemiesCount >= MAX_ENEMIES_COUNT)
                return;

            ObjectPool<IEnemyInstance> pool;
            _enemiesPool.TryGetValue(config.Id, out pool);

            if (pool == null)
                pool = CreatePoolForEnemyType(config);

            IEnemyInstance enemy = pool.Get();
            if (enemy == null)
                return;

            enemy.Spawn(position);
            _enemyAIService.AddEnemy(enemy.MoveController);

            _spawnedEnemiesCount++;
        }

        private ObjectPool<IEnemyInstance> CreatePoolForEnemyType(EnemyStaticConfig config)
        {
            ObjectPool<IEnemyInstance> pool = new ObjectPool<IEnemyInstance>(
                () =>
                {
                    return CreateNewEnemy(config);
                },
                null,
                null,
                OnDestroyEnemy, false, MAX_ENEMIES_COUNT, MAX_ENEMIES_COUNT);

            _enemiesPool.Add(config.Id, pool);
            return pool;
        }

        private IEnemyInstance CreateNewEnemy(EnemyStaticConfig config)
        {
            IEnemyInstance instance = GameObject.Instantiate(_enemyPrefab);
            if (instance == null)
                throw new Exception("[ENEMY] Can't spawn enemy: basic prefab is incorrect!");

            instance.SetupEnemy(config);
            instance.GameObject.transform.SetParent(_enemiesContainer);
            instance.OnEnemyReadyToDie += ReleaseEnemy;

            return instance;
        }

        private void OnDestroyEnemy(IEnemyInstance enemy)
        {
            enemy.OnEnemyReadyToDie -= ReleaseEnemy;
        }

        private void ReleaseEnemy(IEnemyInstance enemy)
        {
            if (_enemiesPool.TryGetValue(enemy.TypeId, out ObjectPool<IEnemyInstance> pool))
                pool.Release(enemy);

              _spawnedEnemiesCount--;
        }
    }
}