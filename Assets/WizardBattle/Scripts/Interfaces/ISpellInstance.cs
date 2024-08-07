using IT.WizardBattle.Data;
using UnityEngine;

namespace IT.WizardBattle.Interfaces
{
    public delegate void SpellHitHandler(SpellData spell, GameObject hitObject, Vector2 hitPosition);

    public interface ISpellInstance 
    {
        public event SpellHitHandler OnHitGameObject;

        public string SpellId { get; }
        public bool Enabled { get; }
                
        public void SetupSpell(SpellData spellData);
        public void CastSpell(Vector2 position, Vector2 direction);
        public void Deinitialize();
    }
}