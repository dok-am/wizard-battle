using IT.CoreLib.Services;
using IT.WizardBattle.Data;

namespace IT.WizardBattle.Services
{
    public class EnemyDataStorage : StorageServiceBase<EnemyData>
    {

        public EnemyDataStorage() 
        {
            _pathToStorage = "Enemies/";
        }
    }
}