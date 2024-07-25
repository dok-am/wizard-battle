using IT.WizardBattle.Data;
using IT.WizardBattle.Game;
using System;
using UnityEngine;

namespace IT.WizardBattle.Interfaces
{
    public interface IEnemyInstance : IDamagable
    {
        public event Action<IEnemyInstance> OnEnemyReadyToDie;
        public event Action<IEnemyInstance, float> OnEnemyHealthChanged;

        public GameObject GameObject { get; }

        //TODO: This is a little incorrect, need to be more abstract
        public CharacterMoveController MoveController { get; }

        public string TypeId { get; }

        public bool Enabled { get; }

        public void SetupEnemy(EnemyStaticData characterData);

        public void Deinitialize();

        public void Spawn(Vector2 spawnPoint);

        public void Die();

    }
}