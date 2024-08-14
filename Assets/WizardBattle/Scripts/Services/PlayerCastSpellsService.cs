using IT.CoreLib.Interfaces;
using IT.WizardBattle.Data;
using IT.WizardBattle.Game;
using IT.WizardBattle.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class PlayerCastSpellsService : IService
    {
        public delegate ISpellInstance CastSpellFunction(SpellConfig spellConfig, Vector2 position, Vector2 direction);

        public event CastSpellFunction RequestCastSpell;
        public event Action<VFX, Vector2> RequestPlayVFX;
        

        private PlayerService _playerService;
        private PlayerInputService _playerInputService;
        private DamageService _damageService;


        public void OnInitialized(IContext context)
        {
            _playerInputService = context.GetService<PlayerInputService>();
            _playerService = context.GetService<PlayerService>();
            _damageService = context.GetService<DamageService>();

            _playerInputService.OnShootPressed += OnShootPressed;
        }

        public void Destroy()
        {
            _playerInputService.OnShootPressed -= OnShootPressed;
        }

        public void ShootWithCurrentSpell()
        {
            Transform shootingPoint = _playerService.PlayerShootingPoint;

            ISpellInstance spellInstance = RequestCastSpell(_playerService.SelectedSpell, 
                shootingPoint.position, 
                shootingPoint.up); ;

            spellInstance.OnHitGameObject += OnSpellHitGameObject;
        }


        private void OnShootPressed()
        {
            ShootWithCurrentSpell();
        }

        private void OnSpellHitGameObject(ISpellInstance spell, GameObject hitObject, Vector2 hitPosition)
        {
            spell.OnHitGameObject -= OnSpellHitGameObject;
            RequestPlayVFX?.Invoke(spell.SpellConfig.HitVFX, hitPosition);
            _damageService.OnSpellHitGameObject(spell.SpellConfig, hitObject, hitPosition);
        }
    }
}