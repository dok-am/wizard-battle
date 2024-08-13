using IT.CoreLib.Interfaces;
using IT.WizardBattle.Data;
using IT.WizardBattle.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class PlayerCastSpellsService : IService
    {
        public event Func<GameObject> RequestSpellInstancePrefab;
        
        private Transform _spellsPoolContainer;

        private PlayerService _playerService;
        private PlayerInputService _playerInputService;
        private DamageService _damageService;
        private VFXService _VFXService;

        private List<ISpellInstance> _spellsPool = new();


        public void OnInitialized(IContext context)
        {
            _spellsPoolContainer = new GameObject("SPELLS_POOL").transform;

            _playerInputService = context.GetService<PlayerInputService>();
            _playerService = context.GetService<PlayerService>();
            _damageService = context.GetService<DamageService>();
            _VFXService = context.GetService<VFXService>();

            _playerInputService.OnShootPressed += OnShootPressed;
        }

        public void Destroy()
        {
            _playerInputService.OnShootPressed -= OnShootPressed;

            foreach(ISpellInstance spellInstance in _spellsPool)
            {
                spellInstance.Deinitialize();
            }
        }

        public void ShootWithCurrentSpell()
        {
            ISpellInstance spellInstance = GetSpellFromPool();

            if (spellInstance == null)
                spellInstance = AddNewSpellInstance();

            SpellConfig spellData = _playerService.SelectedSpell;
            if (!spellData.Id.Equals(spellInstance.SpellId))
                spellInstance.SetupSpell(spellData);

            Transform shootingPoint = _playerService.PlayerShootingPoint;

            spellInstance.CastSpell(shootingPoint.position, shootingPoint.up);
        }


        private ISpellInstance GetSpellFromPool()
        {
            foreach (ISpellInstance instance in _spellsPool)
            {
                if (!instance.Enabled)
                    return instance;
            }

            return null;
        }

        private void OnShootPressed()
        {
            ShootWithCurrentSpell();
        }

        private ISpellInstance AddNewSpellInstance()
        {
            ISpellInstance instance = GameObject.Instantiate(RequestSpellInstancePrefab(), _spellsPoolContainer).GetComponent<ISpellInstance>();
            if (instance == null)
                throw new Exception("[SPELL] Can't instantiate spell: prefab is wrong!");

            _spellsPool.Add(instance);

            instance.OnHitGameObject += OnSpellHitGameObject;

            return instance;
        }

        private void OnSpellHitGameObject(SpellConfig spell, GameObject hitObject, Vector2 hitPosition)
        {
            _VFXService.PlayVisualEffect(spell.HitVFX, hitPosition);
            _damageService.OnSpellHitGameObject(spell, hitObject, hitPosition);
        }
    }
}