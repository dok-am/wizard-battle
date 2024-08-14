using IT.CoreLib.Managers;
using IT.Game.Services;
using IT.WizardBattle.Data;
using IT.WizardBattle.Game.Battle;
using IT.WizardBattle.Interfaces;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace IT.WizardBattle.Game
{
    [RequireComponent(typeof(CharacterMoveController))]
    public class EnemyController : MonoBehaviour, IEnemyInstance, IPoolableObject
    {
        public event Action<IEnemyInstance> OnEnemyReadyToDie;
        public event EnemyHealthChangeHandler OnEnemyHealthChanged;
        public event Action<IPoolableObject> ReleaseFromPool;

        public GameObject GameObject => gameObject;
        public string Id => _enemyData?.EnemyStaticData?.Id;
        public CharacterMoveController MoveController => _moveController;


        [SerializeField] private Transform _visualContainer;

        private CharacterMoveController _moveController;
        private CharacterDamageEffect _characterDamageEffect;

        private EnemyData _enemyData;
        private GameObject _visual;
        private MeleeEnemyAttack _meleeAttack;
                

        public void SetupEnemy(EnemyStaticConfig characterData)
        {
            _enemyData = new EnemyData(characterData);
            _moveController.SetSpeed(characterData.Speed, characterData.RotationSpeed);
            _meleeAttack = new MeleeEnemyAttack(characterData.AttackCooldown, characterData.MeleeDamage);

            ResetVisual();

            _visual = Instantiate(characterData.EnemyPrefab, _visualContainer);
            _characterDamageEffect.UpdateSpriteForced(_visual);
        }

        public void Deinitialize()
        {
            Destroy(gameObject);
        }

        public void Spawn(Vector2 spawnPoint)
        {
            transform.position = spawnPoint;
            gameObject.SetActive(true);
        }

        public void ReceiveDamage(float damage)
        {
            _enemyData.Health = SimpleDamageCalculator.CalculateHealth(_enemyData.Health, damage, _enemyData.EnemyStaticData.Defense);
            _characterDamageEffect.PlayDamageEffect();
            OnEnemyHealthChanged?.Invoke(this, _enemyData.Health, _enemyData.EnemyStaticData.MaxHealth);
            
            if (_enemyData.Health <= 0.0f)
            {
                OnEnemyReadyToDie?.Invoke(this);
                Die();
            }
        }

        public void Die()
        {
            //TODO: add VFX
            _characterDamageEffect.StopDamageEffect();
            gameObject.SetActive(false);
            ReleaseFromPool?.Invoke(this);
        }


        private void ResetVisual()
        {
            if (_visual != null)
                Destroy(_visual);
        }

        private void Awake()
        {
            _moveController = GetComponent<CharacterMoveController>();
            _characterDamageEffect = GetComponent<CharacterDamageEffect>();
        }

        private void Update()
        {
            if (_meleeAttack != null)
                _meleeAttack.Update(Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_meleeAttack == null)
                return;

            IPlayerInstance player = collision.gameObject.GetComponent<IPlayerInstance>();
            if (player == null)
                return;

            _meleeAttack.TouchPlayer(player);
        }
    }
}