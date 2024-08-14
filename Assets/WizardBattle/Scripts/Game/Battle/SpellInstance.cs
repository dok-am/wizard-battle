using IT.CoreLib.Managers;
using IT.WizardBattle.Data;
using IT.WizardBattle.Interfaces;
using System;
using UnityEngine;

namespace IT.WizardBattle.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpellInstance : MonoBehaviour, ISpellInstance, IPoolableObject
    {
        public event SpellHitHandler OnHitGameObject;
        public event Action<IPoolableObject> ReleaseFromPool;

        public SpellConfig SpellConfig => _spellConfig;
        public string Id => _spellConfig?.Id;


        [SerializeField] private Transform _visualsContainer;

        private SpellConfig _spellConfig;

        private GameObject _spellVisuals;
        private Rigidbody2D _rigidbody;
        private CircleCollider2D _circleCollider;
        private bool _isShooting;
        

        public void SetupSpell(SpellConfig spellData)
        {
            _spellConfig = spellData;

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

            gameObject.SetActive(true);
            _isShooting = true;
        }

        public void Deinitialize()
        {
            OnHitGameObject = null;
        }


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _circleCollider = GetComponent<CircleCollider2D>();
        }

        private void Update()
        {
            if (!_isShooting)
                return;

            _rigidbody.MovePosition(_rigidbody.position + _spellConfig.Speed * Time.deltaTime * (Vector2)transform.up);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _isShooting = false;
            gameObject.SetActive(false);
            Vector2 collisionPoint = collision.contactCount > 0 ? collision.contacts[0].point : collision.otherRigidbody.position;
            OnHitGameObject?.Invoke(this, collision.gameObject, collisionPoint);
            ReleaseFromPool?.Invoke(this);
        }
    }
}