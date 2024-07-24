using IT.CoreLib.Interfaces;
using IT.CoreLib.Tools;
using IT.WizardBattle.Game;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class SpawnPointsService : IService
    {
        public Vector2 PlayerSpawnPoint => _playerSpawnPoint.transform.position;
        public Vector2[] EnemiesSpawnPoints => _enemiesSpawnPoints;

        private SpawnPoint[] _allSpawnPoints;
        private SpawnPoint _playerSpawnPoint;
        private Vector2[] _enemiesSpawnPoints;

        public void Initialize()
        {
            _allSpawnPoints = GameObject.FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
            List<Vector2> enemiesSpawnPoints = new();

            for (int i=0; i< _allSpawnPoints.Length; i++)
            {
                SpawnPoint spawnPoint = _allSpawnPoints[i];
                if (spawnPoint.PlayerSpawnPoint)
                {
                    _playerSpawnPoint = spawnPoint;
                }
                else
                {
                    enemiesSpawnPoints.Add(spawnPoint.transform.position);
                }
            }

            _enemiesSpawnPoints = enemiesSpawnPoints.ToArray();

            if (_playerSpawnPoint == null)
                CLDebug.Log("No player spawn point on scene!", "SPAWN", "red");
        }

        public void OnInitialized(IBootstrap bootstrap)
        {
            
        }

        public void Destroy()
        {
            
        }
    }
}
