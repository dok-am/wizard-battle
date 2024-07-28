
namespace IT.WizardBattle.Interfaces
{
    public interface ICharacterData 
    {
        public float MaxHealth { get; }

        /// <summary>
        /// From 0 to 1, where
        /// 0 - immortal
        /// 1 - no defence
        /// </summary>
        public float Defense { get;}
        public float Speed { get; }
        public float RotationSpeed { get; }
        public float MeleeDamage { get; }
    }
}