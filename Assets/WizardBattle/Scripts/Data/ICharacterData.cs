using System.Collections;
using UnityEngine;

namespace IT.WizardBattle.Data
{
    public interface ICharacterData 
    {
        public float Health { get;}
        /// From 0 to 1
        public float Defense { get;}
        public float Speed { get; }
        public float RotationSpeed { get; }
        public float MeleeDamage { get; }
    }
}