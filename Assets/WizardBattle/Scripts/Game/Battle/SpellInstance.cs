using IT.WizardBattle.Data;
using IT.WizardBattle.Interfaces;
using System;
using UnityEngine;

namespace IT.WizardBattle.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpellInstance : MonoBehaviour, ISpellInstance
    {
        public string SpellId => _spellId;

        public bool Enabled {
            get
            {
                return gameObject.activeSelf;
            }
            private set 
            {
                gameObject.SetActive(value);
            }
        }

        [SerializeField] private Transform _visualsContainer;

        private IDamageAction _damageAction;
        private GameObject _spellVisuals;
        private string _spellId;
        private float _speed;

        private Rigidbody2D _rigidbody;
        private CircleCollider2D _circleCollider;
        private bool _isShooting;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _circleCollider = GetComponent<CircleCollider2D>();
        }

        public void SetupSpell(SpellData spellData)
        {
            _spellId = spellData.Id;
            _damageAction = spellData.DamageAction;
            _speed = spellData.Speed;

            _circleCollider.radius = spellData.Radius;

            if (_spellVisuals != null)
            {
                Destroy(_spellVisuals);
                _spellVisuals = null;
            }

            _spellVisuals = Instantiate(spellData.ProjectileVisual, _visualsContainer);
        }

        public void CastSpell(Vector2 position, Vector2 direction)
        {
            float rotation = Vector2.SignedAngle(Vector2.up, direction);
            
            transform.position = position;
            transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);

            Enabled = true;
            _isShooting = true;
        }

        public void Deinitialize()
        {
            Destroy(gameObject);
        }



        private void Update()
        {
            if (!_isShooting)
                return;

            _rigidbody.MovePosition(_rigidbody.position + _speed * Time.deltaTime * (Vector2)transform.up);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //TODO: make normal
            _isShooting = false;
            Enabled = false;
        }
    }
}