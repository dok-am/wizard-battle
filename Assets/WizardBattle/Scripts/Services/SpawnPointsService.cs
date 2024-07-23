using IT.CoreLib.Interfaces;
using IT.CoreLib.Tools;
using IT.WizardBattle.Game;
using System.Linq;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class SpawnPointsService : IService
    {
        public Vector3 GetPlayerSpawnPoint => _playerSpawnPoint.transform.position;

        private SpawnPoint[] _spawnPoints;
        private SpawnPoint _playerSpawnPoint;

        public void Initialize()
        {
            _spawnPoints = GameObject.FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
            _playerSpawnPoint = _spawnPoints.FirstOrDefault(point => point.PlayerSpawnPoint == true);

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
