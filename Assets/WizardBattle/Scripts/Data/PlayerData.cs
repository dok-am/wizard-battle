﻿using System.Collections.Generic;

namespace IT.WizardBattle.Data
{
    public class PlayerData : ICharacterData
    {
        public float Health { get; set; }

        public float Defense { get; set; }

        public float Speed { get; set; }

        public float RotationSpeed { get; set; }

        public float MeleeDamage { get; set; }

        public List<SpellData> AvailableSpells { get; } = new List<SpellData>();
    }
}