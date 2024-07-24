using IT.CoreLib.Application;
using IT.WizardBattle.Services;
using UnityEngine;

namespace IT.WizardBattle.Application
{
    public class GameplayBootstrap : SceneBootstrap
    {

        [SerializeField] private PlayerService _playerServicePrefab;
        
        protected override void InitializeServices()
        {
            AddService<PlayerInputService>();
            AddService<SpellDataStorage>();
            AddService<SpawnPointsService>();
            AddService<PlayerService>(_playerServicePrefab.gameObject);
            AddService<CameraService>();
        }

        protected override void InitializeScene()
        {

        }
    }
}
