using IT.CoreLib.Interfaces;
using IT.WizardBattle.Data;
using IT.WizardBattle.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class EnemySpawnerService : MonoBehaviour, IService
    {
        [SerializeField] private GameObject _enemyInstancePrefab;
        [SerializeField] private float _spawnerCooldownTime = 1.0f;
        [SerializeField] private int _maximalEnemiesCount = 10;

        private Vector2[] _spawnPoints;
        private EnemyData[] _enemiesData;

        private bool _isSpawning;
        private float _spawnTimer;
        private List<IEnemyInstance> _enemiesPool = new();

        public void Initialize()
        {
            _isSpawning = false;
        }

        public void OnInitialized(IBootstrap bootstrap)
        {
            _spawnPoints = bootstrap.GetService<SpawnPointsService>().EnemiesSpawnPoints;
            _enemiesData = bootstrap.GetService<EnemyDataStorage>().GetAllModels();
        }

        public void Destroy()
        {
            
        }

        public void StartSpawning()
        {
            _isSpawning = true;
            _spawnTimer = 0.0f;
        }


        private void Update()
        {
            if (!_isSpawning)
                return;

            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= _spawnerCooldownTime)
            {
                SpawnRandomEnemy();
                _spawnTimer = 0.0f;
            }
        }

        private void SpawnRandomEnemy()
        {
            if (!IsEnemyAvailable())
                return;

            EnemyData enemyData = GetRandomItem(_enemiesData);

            IEnemyInstance instance = GetEnemyFromPool(enemyData.Id);
            if (instance == null) 
            {
                instance = CreateNewEnemy();
            }

            instance.SetupEnemy(enemyData);
            instance.Spawn(GetRandomItem(_spawnPoints));
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

                if (activeEnemies >= _maximalEnemiesCount)
                    return false;
            }

            return true;
        }

        private IEnemyInstance CreateNewEnemy()
        {
            IEnemyInstance instance = Instantiate(_enemyInstancePrefab).GetComponent<IEnemyInstance>();
            if (instance == null)
                throw new Exception("[ENEMY] Can't spawn enemy: basic prefab is incorrect!");

            _enemiesPool.Add(instance);

            return instance;
        }

        private T GetRandomItem<T>(T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }
    }
}