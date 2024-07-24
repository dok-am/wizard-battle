using IT.WizardBattle.Services;
using UnityEngine;

namespace IT.WizardBattle.Interfaces
{
    public interface IPlayerInstance : ICharacterInstance
    {
        public GameObject GameObject { get; }

        public void Initialize(PlayerService playerService, PlayerInputService playerInputService, ICharacterData characterData);

        public void Deinitialize();

        public Transform ShootingPoint { get; }

    }
}