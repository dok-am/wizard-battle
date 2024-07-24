using IT.WizardBattle.Data;
using IT.WizardBattle.Interfaces;
using UnityEngine;

namespace IT.WizardBattle.Game
{
    [CreateAssetMenu(fileName = "SimpleDamageAction", menuName = "Wizard Battle/Damage Actions/Simple damage action")]
    public class SimpleDamageAction : DamageActionBase
    {
        public override bool PerformDamage(ICharacterInstance character, float damage)
        {
            character.ReceiveDamage(damage);
            return true;
        }
    }
}