﻿using IT.WizardBattle.Services;
using UnityEngine;

namespace IT.WizardBattle.Interfaces
{
    public interface IPlayerInstance : IDamagable
    {
        public GameObject GameObject { get; }

        public void Initialize(PlayerService playerService, PlayerInputService playerInputService, ICharacterData characterData);

        public void Deinitialize();

        public Transform ShootingPoint { get; }

        public void Die();

    }
}