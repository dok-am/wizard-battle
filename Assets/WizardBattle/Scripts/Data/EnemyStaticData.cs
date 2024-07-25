using IT.CoreLib.Interfaces;
using IT.WizardBattle.Interfaces;
using System.Collections;
using UnityEngine;

namespace IT.WizardBattle.Data
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Wizard Battle/Enemy")]
    public class EnemyStaticData : ScriptableObject, ICharacterData, IStaticModel
    {
        public string Id => _typeId;
        public float Health => _health;
        public float Defense => _defense;
        public float Speed => _speed;
        public float RotationSpeed => _rotationSpeed;
        public float MeleeDamage => _meleeDamage;

        public GameObject EnemyPrefab => _enemyPrefab;

        

        [Header("Data")]
        [SerializeField] private string _typeId;
        [SerializeField] private float _health;
        [SerializeField, Range(0.0f, 1.0f)] private float _defense;
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _meleeDamage;

        [Header("Prefab")]
        [SerializeField] private GameObject _enemyPrefab;
    }
}