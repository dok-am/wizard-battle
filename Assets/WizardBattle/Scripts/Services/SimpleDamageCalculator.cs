
namespace IT.Game.Services
{
    public static class SimpleDamageCalculator 
    {
        public static float CalculateHealth(float previousHealth, float damage, float defence)
        {
            float result = previousHealth - damage * defence;
            return result < 0.0f ? 0.0f : result;
        }
    }
}