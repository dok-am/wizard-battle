using IT.Game.Services;
using IT.WizardBattle.Data;
using IT.WizardBattle.Interfaces;
using System;
using UnityEngine;

namespace IT.WizardBattle.Game
{
    [RequireComponent(typeof(CharacterMoveController))]
    public class EnemyController : MonoBehaviour, IEnemyInstance
    {
        public event Action<IEnemyInstance> OnEnemyReadyToDie;
        public event Action<IEnemyInstance, float> OnEnemyHealthChanged;

        public GameObject GameObject => gameObject;
        public string TypeId => _enemyData != null ? _enemyData.EnemyStaticData.Id : null;

        public bool Enabled
        {
            get
            {
                return gameObject.activeSelf;
            }
            private set
            {
                gameObject.SetActive(value);
            }
        }

        public CharacterMoveController MoveController => _moveController;

        [SerializeField] private Transform _visualContainer;

        private CharacterMoveController _moveController;

        private EnemyData _enemyData;
        private GameObject _visual;

        private void Awake()
        {
            _moveController = GetComponent<CharacterMoveController>();
        }

        public void SetupEnemy(EnemyStaticData characterData)
        {
            _enemyData = new EnemyData(characterData);
            _moveController.SetSpeed(characterData.Speed, characterData.RotationSpeed);

            ResetVisual();

            _visual = Instantiate(characterData.EnemyPrefab, _visualContainer);
        }

        public void Deinitialize()
        {
            Destroy(gameObject);
        }

        public void Spawn(Vector2 spawnPoint)
        {
            transform.position = spawnPoint;
            Enabled = true;
        }

        public void ReceiveDamage(float damage)
        {
            _enemyData.Health = SimpleDamageCalculator.CalculateHealth(_enemyData.Health, damage, _enemyData.EnemyStaticData.Defense);
            if (_enemyData.Health == 0.0f)
            {
                OnEnemyReadyToDie?.Invoke(this);
                Die();
            }
        }

        public void Die()
        {
            //TODO: add VFX
            Enabled = false;
        }



        private void ResetVisual()
        {
            if (_visual != null)
                Destroy(_visual);
        }

        
    }
}