

namespace IT.WizardBattle.Data
{
    public class EnemyData 
    {
        public EnemyStaticData EnemyStaticData { get; private set; }
        
        public float Health { 
            get
            {
                return _health;
            }
            set
            {
                _health = value < 0.0f ? 0.0f : value;
            }
        }

        private float _health;

        public EnemyData(EnemyStaticData enemyStaticData)
        {
            EnemyStaticData = enemyStaticData;
            _health = enemyStaticData.Health;
        }
    }
}