using IT.CoreLib.Interfaces;
using IT.CoreLib.Managers;
using System;
using UnityEngine;

namespace IT.WizardBattle.Game
{
    public class VFX : MonoBehaviour, IPoolableObject
    {
        public event Action<IPoolableObject> ReleaseFromPool;

        public string Id => _id;
        public bool IsPlaying => _isPlaying;


        [SerializeField] private string _id;
        [SerializeField] private float _lifeTime;
        [SerializeField] private ParticleSystem _particleSystem;

        private float _timer;
        private bool _isPlaying;


        public void Play(Vector2 position)
        {
            gameObject.SetActive(true);
            transform.position = position;
            _timer = 0;
            _particleSystem.Play();
            _isPlaying = true;
        }

        public void Stop()
        {
            _timer = 0;
            _particleSystem.Stop();
            _isPlaying = false;
            gameObject.SetActive(false);
            ReleaseFromPool?.Invoke(this);
        }


        private void Update()
        {
            if (!_isPlaying)
                return;

            _timer += Time.deltaTime;

            if (_timer >= _lifeTime)
                Stop();
        }
    }
}