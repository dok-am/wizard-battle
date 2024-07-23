using IT.CoreLib.Services;
using IT.WizardBattle.Data;

namespace IT.WizardBattle.Services
{
    public class SpellDataStorage : StorageServiceBase<SpellData>
    {
        public SpellDataStorage()
        {
            _pathToStorage = "Spells/";
        }
    }
}
