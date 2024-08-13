using IT.CoreLib.Services;
using IT.WizardBattle.Data;

namespace IT.WizardBattle.Services
{
    public class EnemyConfigStorage : StaticConfigStorageBase<EnemyStaticConfig>
    {
        public EnemyConfigStorage() 
        {
            _pathToStorage = "Enemies/";
        }
    }
}