using IT.WizardBattle.Data;
using IT.WizardBattle.Game;
using System;
using UnityEngine;

namespace IT.WizardBattle.Interfaces
{

    public delegate void EnemyHealthChangeHandler(IEnemyInstance enemy, float health, float maxHealth);

    public interface IEnemyInstance : IDamagable
    {
        public event Action<IEnemyInstance> OnEnemyReadyToDie;
        public event EnemyHealthChangeHandler OnEnemyHealthChanged;

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