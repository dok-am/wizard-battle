using UnityEngine;

namespace IT.WizardBattle.Game
{
    public class VFX : MonoBehaviour
    {
        public string Id => _id;
        public bool IsPlaying => _isPlaying;

        public bool Enabled  {
            get 
            {
                return gameObject != null && gameObject.activeSelf;
            }
            private set 
            {
                if (gameObject != null)
                    gameObject.SetActive(value);
            }
        }


        [SerializeField] private string _id;
        [SerializeField] private float _lifeTime;
        [SerializeField] private ParticleSystem _particleSystem;

        private float _timer;
        private bool _isPlaying;

        public void Play(Vector2 position)
        {
            Enabled = true;
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
            Enabled = false;
        }

        public void Update()
        {
            if (!_isPlaying)
                return;

            _timer += Time.deltaTime;

            if (_timer >= _lifeTime)
            {
                Stop();
            }
        }
    }
}