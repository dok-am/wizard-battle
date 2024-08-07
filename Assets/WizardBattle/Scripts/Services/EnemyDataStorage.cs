using IT.CoreLib.Services;
using IT.WizardBattle.Data;

namespace IT.WizardBattle.Services
{
    public class EnemyDataStorage : StorageServiceBase<EnemyStaticData>
    {
        public EnemyDataStorage() 
        {
            _pathToStorage = "Enemies/";
        }
    }
}