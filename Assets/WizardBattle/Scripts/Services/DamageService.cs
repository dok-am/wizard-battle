using IT.CoreLib.Interfaces;
using IT.WizardBattle.Data;
using IT.WizardBattle.Interfaces;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class DamageService : IService
    {
        public void OnSpellHitGameObject(SpellData spell, GameObject gameObject, Vector2 position)
        {
            IDamagable damagable = gameObject.GetComponent<IDamagable>();
            if (damagable == null)
                return;

            spell.DamageAction.PerformDamage(damagable, spell.Damage);
        }
    }
}