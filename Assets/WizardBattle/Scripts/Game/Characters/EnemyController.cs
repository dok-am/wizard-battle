using IT.WizardBattle.Data;
using IT.WizardBattle.Interfaces;
using UnityEngine;

namespace IT.WizardBattle.Game
{
    [RequireComponent(typeof(CharacterMoveController))]
    public class EnemyController : MonoBehaviour, IEnemyInstance
    {
        public GameObject GameObject => gameObject;
        public string TypeId => _typeId;

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

        private string _typeId;
        private GameObject _visual;

        private void Awake()
        {
            _moveController = GetComponent<CharacterMoveController>();
        }

        public void SetupEnemy(EnemyData characterData)
        {
            _moveController.SetSpeed(characterData.Speed, characterData.RotationSpeed);
            _typeId = characterData.Id;

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
            
        }



        private void ResetVisual()
        {
            if (_visual != null)
                Destroy(_visual);
        }

        
    }
}