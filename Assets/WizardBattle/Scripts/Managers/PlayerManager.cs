using IT.CoreLib.Interfaces;
using IT.WizardBattle.Game.Player;
using IT.WizardBattle.Interfaces;
using IT.WizardBattle.Services;
using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace IT.WizardBattle.Managers
{
    public class PlayerManager : IManager, IUpdatable
    {
        public event Action<IPlayerInstance> OnPlayerSpawned;


        private PlayerController _playerPrefab;
        private PlayerService _playerService;
        private PlayerInputService _playerInputService;
        private PlayerController _playerInstance;
        private SpawnPointsManager _spawnPointsManager;


        public PlayerManager(PlayerController playerPrefab, 
            PlayerService playerService, 
            PlayerInputService inputService, 
            SpawnPointsManager spawnPointsManager)
        {
            _playerPrefab = playerPrefab;
            _playerService = playerService;
            _playerInputService = inputService;
            _spawnPointsManager = spawnPointsManager;

            _playerService.OnPlayerDied += Die;
            _playerService.RequestRespawnPlayer += RespawnPlayer;
            _playerService.RequestPlayerInstance += PlayerInstance;
        }

        public void Unbind()
        {
            _playerService.OnPlayerDied -= Die;
            _playerService.RequestRespawnPlayer -= RespawnPlayer;
            _playerService.RequestPlayerInstance -= PlayerInstance;
        }

        public IPlayerInstance PlayerInstance() => _playerInstance;

        public void RespawnPlayer(ICharacterData characterData)
        {
            if (_playerInstance != null)
                DestroyPlayer();

            _playerInstance = GameObject.Instantiate(_playerPrefab, 
                _spawnPointsManager.PlayerSpawnPoint.position,
                Quaternion.identity)
                .GetComponent<PlayerController>();

            if (_playerInstance == null)
                throw new Exception("[PLAYER] Player's prefab is wrong!");

            _playerInstance.Initialize(_playerService, characterData);
            OnPlayerSpawned?.Invoke(_playerInstance);
        }      

        public void Die()
        {
            DestroyPlayer();
        }

        public void Update(float dt)
        {
            if (_playerInstance == null)
                return;

            _playerInstance.UpdateInput(_playerInputService.MoveValue, _playerInputService.RotateValue);
        }


        private void DestroyPlayer()
        {
            if (_playerInstance != null)
                GameObject.Destroy(_playerInstance.gameObject);

            _playerInstance = null;
        }
    }
}