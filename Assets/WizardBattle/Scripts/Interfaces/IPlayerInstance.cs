using IT.WizardBattle.Services;
using UnityEngine;

namespace IT.WizardBattle.Interfaces
{
    public interface IPlayerInstance : IDamagable
    {
        public GameObject GameObject { get; }
        public Transform ShootingPoint { get; }

        public void Initialize(PlayerService playerService, ICharacterData characterData);
        
        public void Die();
    }
}