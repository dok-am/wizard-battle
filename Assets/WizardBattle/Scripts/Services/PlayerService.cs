using IT.CoreLib.Interfaces;
using IT.WizardBattle.Player;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class PlayerService : MonoBehaviour, IService
    {
        [SerializeField] private PlayerController _playerPrefab;

        private PlayerController _player;

        private SpawnPointsService _spawnPointsService;

        public void Initialize()
        {
            
        }

        public void OnInitialized(IBootstrap bootstrap)
        {
            _spawnPointsService = bootstrap.GetService<SpawnPointsService>();
            RespawnPlayer(_spawnPointsService.GetPlayerSpawnPoint);
        }

        public void Destroy()
        {
            
        }

        public void RespawnPlayer(Vector3 position)
        {
            if (_player != null)
                DestroyPlayer();

            _player = Instantiate(_playerPrefab.gameObject, position, Quaternion.identity).GetComponent<PlayerController>();
        }

        public void DestroyPlayer()
        {
            Destroy(_player.gameObject);
            _player = null;
        }
    }
}
