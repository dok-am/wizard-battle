using IT.WizardBattle.Data;
using UnityEngine;

namespace IT.WizardBattle.Interfaces
{
    public interface ISpellInstance 
    {
        public string SpellId { get; }
        public bool Enabled { get; set; }

        public void SetupSpell(SpellData spellData);
        public void StartShooting(Vector2 position, Vector2 direction);

        public void Deinitialize();
       
    }
}