using IT.CoreLib.Interfaces;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class CameraService : IService, IUpdatable
    {
        private const float CAMERA_SMOOTH_SPEED = 10.0f;

        private Camera _camera;
        private Transform _targetToFollow;

        private float _cameraPozitionZ;

        public void Initialize()
        {
            //If we'll want to improve camera system we could start here
            _camera = Camera.main;
            _cameraPozitionZ = _camera.transform.position.z;
        }

        public void OnInitialized(IBootstrap bootstrap)
        {
            
        }

        public void Destroy()
        {
            
        }

        public void SetTarget(Transform target)
        {
            _targetToFollow = target;
        }

        public void Update(float dt)
        {
            if (_targetToFollow == null)
                return;

            Vector3 plainPosition = _targetToFollow.position;
            plainPosition.z = _cameraPozitionZ;

            _camera.transform.position = Vector3.Lerp(_camera.transform.position, plainPosition, dt * CAMERA_SMOOTH_SPEED);
        }
    }
}
