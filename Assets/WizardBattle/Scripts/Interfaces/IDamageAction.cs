namespace IT.WizardBattle.Interfaces
{
    public interface IDamageAction 
    {
        public bool PerformDamage(IDamagable character, float damage);
    }
}