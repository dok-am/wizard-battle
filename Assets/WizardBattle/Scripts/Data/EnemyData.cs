using UnityEngine;

namespace IT.WizardBattle.Data
{
    public class EnemyData 
    {
        public EnemyStaticConfig EnemyStaticData { get; private set; }
        
        public float Health { 
            get
            {
                return _health;
            }
            set
            {
                _health = Mathf.Clamp(value, 0.0f, EnemyStaticData.MaxHealth);
            }
        }

        private float _health;


        public EnemyData(EnemyStaticConfig enemyStaticData)
        {
            EnemyStaticData = enemyStaticData;
            _health = enemyStaticData.MaxHealth;
        }
    }
}