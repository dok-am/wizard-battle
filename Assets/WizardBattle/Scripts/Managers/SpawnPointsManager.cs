using IT.CoreLib.Extensions;
using IT.CoreLib.Interfaces;
using System.Collections;
using UnityEngine;

namespace IT.WizardBattle.Managers
{
    public class SpawnPointsManager : IManager
    {
        public Transform PlayerSpawnPoint { get; private set; }
        public Transform[] EnemiesSpawnPoints { get; private set; }

        public SpawnPointsManager(Transform playerSpawnPoint, Transform[] enemiesSpawnPoints)
        {
            PlayerSpawnPoint = playerSpawnPoint;
            EnemiesSpawnPoints = enemiesSpawnPoints;
        }

        public Vector2 GetRandomEnemySpawnPoint()
        {
            return EnemiesSpawnPoints.GetRandomItem().position;
        }
    }
}