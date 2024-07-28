using IT.CoreLib.Interfaces;
using IT.WizardBattle.Interfaces;
using UnityEngine;

namespace IT.WizardBattle.Data
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Wizard Battle/Enemy")]
    public class EnemyStaticData : ScriptableObject, ICharacterData, IStaticModel
    {
        public string Id => _typeId;
        public float MaxHealth => _maxHealth;
        public float Defense => _defense;
        public float Speed => _speed;
        public float RotationSpeed => _rotationSpeed;
        public float MeleeDamage => _meleeDamage;
        public float AttackCooldown => _attackCooldown;
        public GameObject EnemyPrefab => _enemyPrefab;


        [Header("Data")]
        [SerializeField] private string _typeId;
        [SerializeField] private float _maxHealth;
        [SerializeField, Range(0.0f, 1.0f)] private float _defense;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _meleeDamage;
        [SerializeField] private float _attackCooldown;

        [Header("Prefab")]
        [SerializeField] private GameObject _enemyPrefab;
    }
}