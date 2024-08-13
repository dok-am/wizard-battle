using IT.CoreLib.Services;
using IT.WizardBattle.Data;

namespace IT.WizardBattle.Services
{
    public class SpellConfigStorage : StaticConfigStorageBase<SpellConfig>
    {
        public SpellConfigStorage()
        {
            _pathToStorage = "Spells";
        }
    }
}
