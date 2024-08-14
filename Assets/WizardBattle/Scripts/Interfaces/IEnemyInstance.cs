using IT.CoreLib.Interfaces;
using IT.WizardBattle.Data;
using System;
using UnityEngine;

namespace IT.WizardBattle.Interfaces
{
    public delegate void EnemyHealthChangeHandler(IEnemyInstance enemy, float health, float maxHealth);

    public interface IEnemyInstance : IDamagable, IIdentifiable
    {
        public event Action<IEnemyInstance> OnEnemyReadyToDie;
        public event EnemyHealthChangeHandler OnEnemyHealthChanged;

        public GameObject GameObject { get; }

        public void SetupEnemy(EnemyStaticConfig characterData);
        public void Deinitialize();
        public void Spawn(Vector2 spawnPoint);
        public void Die();
    }
}