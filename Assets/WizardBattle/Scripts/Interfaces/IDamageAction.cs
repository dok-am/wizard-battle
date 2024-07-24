using UnityEngine;

namespace IT.WizardBattle.Interfaces
{
    public interface IDamageAction 
    {
        public bool PerformDamage(ICharacterInstance character, float damage);
    }
}