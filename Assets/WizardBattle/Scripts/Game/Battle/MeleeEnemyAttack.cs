using IT.CoreLib.Interfaces;
using IT.WizardBattle.Interfaces;

namespace IT.WizardBattle.Game.Battle
{
    public class MeleeEnemyAttack : IUpdatable
    {
        public MeleeEnemyAttack(float cooldown, float damage)
        {
            _attackCooldown = cooldown;
            _damage = damage;
            _timer = 0.0f;
        }

        private float _attackCooldown;
        private float _damage;
        private float _timer;

        public void Update(float dt)
        {
            _timer += dt;
        }

        public void TouchPlayer(IPlayerInstance player)
        {
            if (_timer > _attackCooldown)
            {
                _timer = 0.0f;
                Attack(player);
            }
        }

        private void Attack(IPlayerInstance player)
        {
            player.ReceiveDamage(_damage);
        }

    }
}