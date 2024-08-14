using IT.CoreLib.Interfaces;
using IT.WizardBattle.Data;
using UnityEngine;

namespace IT.WizardBattle.Interfaces
{
    public delegate void SpellHitHandler(ISpellInstance spell, GameObject hitObject, Vector2 hitPosition);

    public interface ISpellInstance : IIdentifiable
    {
        public event SpellHitHandler OnHitGameObject;

        public SpellConfig SpellConfig { get; }
                
        public void SetupSpell(SpellConfig spellData);
        public void CastSpell(Vector2 position, Vector2 direction);
        public void Deinitialize();
    }
}