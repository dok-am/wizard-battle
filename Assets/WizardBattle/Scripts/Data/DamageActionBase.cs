using IT.WizardBattle.Interfaces;
using UnityEngine;

namespace IT.WizardBattle.Data
{
    public abstract class DamageActionBase : ScriptableObject, IDamageAction
    {
        public abstract bool PerformDamage(ICharacterInstance character, float damage);
    }
}