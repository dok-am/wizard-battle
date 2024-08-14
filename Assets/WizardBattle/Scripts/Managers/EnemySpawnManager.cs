using IT.CoreLib.Scripts;
using IT.WizardBattle.Data;
using IT.WizardBattle.Game;
using IT.WizardBattle.Services;
using UnityEngine;

namespace IT.WizardBattle.Managers
{
    public class EnemySpawnManager : TypedPoolSpawnManager<EnemyController, EnemyStaticConfig>
    {
        private EnemyController _enemyPrefab;
        private EnemyAIService _enemyAIService;

        
        public EnemySpawnManager(EnemyController enemyPrefab, EnemyAIService aiService)
        {
            _containerName = "ENEMIES_POOL";
            _maxCount = 10;

            _enemyPrefab = enemyPrefab;
            _enemyAIService = aiService;

            Initialize();
        }

        public void SpawnEnemyIfPossible(EnemyStaticConfig config, Vector2 position)
        {
            EnemyController enemy = Spawn(_enemyPrefab, config);

            if (enemy != null)
            {
                enemy.Spawn(position);
                _enemyAIService.AddEnemy(enemy.MoveController);
            }
        }

        protected override void OnObjectCreated(EnemyController instance, EnemyStaticConfig config)
        {
            instance.SetupEnemy(config);
        }        
    }
}