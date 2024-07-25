using IT.WizardBattle.Data;
using IT.WizardBattle.Interfaces;
using System;
using UnityEngine;

namespace IT.WizardBattle.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpellInstance : MonoBehaviour, ISpellInstance
    {
        public event Action<SpellData, GameObject, Vector2> OnHitGameObject;

        public string SpellId => _spellData != null ? _spellData.Id : null;

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

        private SpellData _spellData;

        private GameObject _spellVisuals;
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
            //TODO: Maybe, store the whole data here?
            _spellData = spellData;

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
            OnHitGameObject = null;
            Destroy(gameObject);
        }



        private void Update()
        {
            if (!_isShooting)
                return;

            _rigidbody.MovePosition(_rigidbody.position + _spellData.Speed * Time.deltaTime * (Vector2)transform.up);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _isShooting = false;
            Enabled = false;
            Vector2 collisionPoint = collision.contactCount > 0 ? collision.contacts[0].point : collision.otherRigidbody.position;
            OnHitGameObject?.Invoke(_spellData, collision.gameObject, collisionPoint);
        }
    }
}