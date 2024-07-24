using IT.CoreLib.Interfaces;
using IT.WizardBattle.Data;
using IT.WizardBattle.Player;
using System;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class PlayerService : MonoBehaviour, IService
    {
        [SerializeField] private PlayerController _playerPrefab;

        public event Action<SpellData> OnSpellSelected;

        public ICharacterData PlayerData => _playerData;
        public SpellData[] AvailableSpells => _playerData.AvailableSpells.ToArray();
        public SpellData SelectedSpell => _selectedSpell;


        private PlayerData _playerData;
        private SpellData _selectedSpell;

        private PlayerController _player;
        
        private SpawnPointsService _spawnPointsService;
        private PlayerInputService _playerInputService;



        public void Initialize()
        {
            
        }

        public void OnInitialized(IBootstrap bootstrap)
        {
            LoadPlayerData(bootstrap.GetService<SpellDataStorage>());

            _spawnPointsService = bootstrap.GetService<SpawnPointsService>();
            _playerInputService = bootstrap.GetService<PlayerInputService>();

            RespawnPlayer(_spawnPointsService.GetPlayerSpawnPoint);

            bootstrap.GetService<CameraService>().SetTarget(_player.transform);
        }

        public void Destroy()
        {
            DestroyPlayer();
        }



        public void RespawnPlayer(Vector3 position)
        {
            if (_player != null)
                DestroyPlayer();

            _player = Instantiate(_playerPrefab.gameObject, position, Quaternion.identity).GetComponent<PlayerController>();
            _player.Initialize(this, _playerInputService, _playerData);
        }

        public void SelectNextSpell()
        {
            int spellIndex = _playerData.AvailableSpells.IndexOf(_selectedSpell) + 1;
            if (spellIndex >= _playerData.AvailableSpells.Count)
            {
                spellIndex = 0;
            }

            _selectedSpell = _playerData.AvailableSpells[spellIndex];
            OnSpellSelected?.Invoke(_selectedSpell);
        }

        public void SelectPreviousSpell()
        {
            int spellIndex = _playerData.AvailableSpells.IndexOf(_selectedSpell) - 1;
            if (spellIndex < 0)
            {
                spellIndex = _playerData.AvailableSpells.Count - 1;
            }

            _selectedSpell = _playerData.AvailableSpells[spellIndex];
            OnSpellSelected?.Invoke(_selectedSpell);
        }



        private void DestroyPlayer()
        {
            Destroy(_player.gameObject);
            _player = null;
        }

        private void LoadPlayerData(SpellDataStorage spellStorage)
        {
            //TODO: load it from save/config

            _playerData = new PlayerData()
            {
                Health = 100.0f,
                Defense = 1.0f,
                MeleeDamage = 0.0f,
                Speed = 5.0f,
                RotationSpeed = 200.0f
            };

            //For now, just adding everything we have in storage
            _playerData.AvailableSpells.AddRange(spellStorage.GetAllModels());
            _selectedSpell = _playerData.AvailableSpells[0];
        }

    }
}
