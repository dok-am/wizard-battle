using System.Collections.Generic;
using IT.WizardBattle.Interfaces;

namespace IT.WizardBattle.Data
{
    public class PlayerData : ICharacterData
    {
        public string TypeId => "Player";
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public float Defense { get; set; }
        public float Speed { get; set; }
        public float RotationSpeed { get; set; }
        public float MeleeDamage { get; set; }
        public List<SpellConfig> AvailableSpells { get; } = new List<SpellConfig>();
    }
}