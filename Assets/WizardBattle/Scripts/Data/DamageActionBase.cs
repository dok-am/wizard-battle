using IT.WizardBattle.Interfaces;
using UnityEngine;

namespace IT.WizardBattle.Data
{
    public abstract class DamageActionBase : ScriptableObject, IDamageAction
    {
        public abstract bool PerformDamage(IDamagable character, float damage);
    }
}