using IT.WizardBattle.Data;
using UnityEngine;

namespace IT.WizardBattle.Interfaces
{
    public interface IEnemyInstance : ICharacterInstance
    {
        public GameObject GameObject { get; }

        public string TypeId { get; }

        public bool Enabled { get; }

        public void SetupEnemy(EnemyData characterData);

        public void Deinitialize();

        public void Spawn(Vector2 spawnPoint);
        
    }
}