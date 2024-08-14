using IT.CoreLib.Scripts;
using IT.WizardBattle.Data;
using IT.WizardBattle.Game;
using IT.WizardBattle.Interfaces;
using UnityEngine;

namespace IT.WizardBattle.Managers
{
    public class SpellCastManager : TypedPoolSpawnManager<SpellInstance, SpellConfig>
    {
        private SpellInstance _spellPrefab;

        public SpellCastManager(SpellInstance spellPrefab)
        {
            _spellPrefab = spellPrefab;

            _containerName = "SPELLS_CONTAINER";
            _maxCount = 20;

            Initialize();
        }

        public ISpellInstance CastSpell(SpellConfig spellConfig, Vector2 position, Vector2 direction)
        {
            SpellInstance spell = Spawn(_spellPrefab, spellConfig);
            spell.CastSpell(position, direction);

            return spell;
        }

        protected override void OnObjectCreated(SpellInstance obj, SpellConfig config)
        {
            obj.SetupSpell(config);
        }
    }
}