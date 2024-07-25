using IT.CoreLib.Interfaces;
using IT.WizardBattle.Data;
using IT.WizardBattle.Interfaces;
using System.Collections;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class DamageService : IService
    {

        //private PlayerService _playerService;

        public void Initialize()
        {
            
        }

        public void OnInitialized(IBootstrap bootstrap)
        {

        }

        public void Destroy()
        {
            
        }

        public void OnSpellHitGameObject(SpellData spell, GameObject gameObject, Vector2 position)
        {
            //TODO: Run some VFX here

            IDamagable damagable = gameObject.GetComponent<IDamagable>();
            if (damagable == null)
                return;

            spell.DamageAction.PerformDamage(damagable, spell.Damage);
        }
    }
}