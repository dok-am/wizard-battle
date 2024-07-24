using IT.WizardBattle.Data;
using IT.WizardBattle.Game;
using UnityEngine;

namespace IT.WizardBattle.Interfaces
{
    public interface IEnemyInstance : ICharacterInstance
    {
        public GameObject GameObject { get; }

        //TODO: This is a little incorrect, need to be more abstract
        public CharacterMoveController MoveController { get; }

        public string TypeId { get; }

        public bool Enabled { get; }

        public void SetupEnemy(EnemyData characterData);

        public void Deinitialize();

        public void Spawn(Vector2 spawnPoint);

    }
}