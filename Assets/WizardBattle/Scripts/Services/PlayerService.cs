using System;
using UnityEngine;
using IT.CoreLib.Interfaces;
using IT.WizardBattle.Data;
using IT.WizardBattle.Interfaces;
using IT.Game.Services;


namespace IT.WizardBattle.Services
{
    public class PlayerService : IService
    {
        public delegate void PlayerHealthChangedHandler(float health, float maxHealth);

        public event Action<SpellConfig> OnSpellSelected;
        public event PlayerHealthChangedHandler OnPlayerHealthChanged;
        public event Action OnPlayerDied;

        public event Func<IPlayerInstance> RequestPlayerInstance;
        public event Action<ICharacterData> RequestRespawnPlayer;

        public ICharacterData PlayerData => _playerData;
        public SpellConfig[] AvailableSpells => _playerData.AvailableSpells.ToArray();
        public SpellConfig SelectedSpell => _selectedSpell;
        public Transform PlayerShootingPoint => RequestPlayerInstance?.Invoke().ShootingPoint;
        public Transform PlayerTransform => RequestPlayerInstance?.Invoke().GameObject.transform;


        private PlayerData _playerData;
        private SpellConfig _selectedSpell;
        
        private PlayerInputService _playerInputService;

        public void OnInitialized(IContext context)
        {
            LoadPlayerData(context.GetService<SpellConfigStorage>());

            _playerInputService = context.GetService<PlayerInputService>();
            _playerInputService.OnPreviousSpellPressed += SelectPreviousSpell;
            _playerInputService.OnNextSpellPressed += SelectNextSpell;
        }

        public void OnDestroy()
        {
            _playerInputService.OnPreviousSpellPressed -= SelectPreviousSpell;
            _playerInputService.OnNextSpellPressed -= SelectNextSpell;
        }

        public void RespawnPlayer()
        {
            RequestRespawnPlayer(_playerData);
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

        public void AddDamage(float damage)
        {
            _playerData.Health = SimpleDamageCalculator.CalculateHealth(_playerData.Health, damage, _playerData.Defense);
            OnPlayerHealthChanged?.Invoke(_playerData.Health, _playerData.MaxHealth);

            if (_playerData.Health <= 0.0f) {
                
                OnPlayerDied?.Invoke();
            }
        }


        private void LoadPlayerData(SpellConfigStorage spellStorage)
        {
            //TODO: load it from save/config

            _playerData = new PlayerData()
            {
                Health = 100.0f,
                MaxHealth = 100.0f,
                Defense = 0.0f,
                MeleeDamage = 0.0f,
                Speed = 5.0f,
                RotationSpeed = 200.0f
            };

            //For now, just adding everything we have in storage
            _playerData.AvailableSpells.AddRange(spellStorage.GetAllConfigs());
            _selectedSpell = _playerData.AvailableSpells[0];
        }
    }
}
