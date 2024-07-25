using IT.CoreLib.Application;
using IT.CoreLib.UI;
using IT.WizardBattle.Data;
using IT.WizardBattle.Services;
using UnityEngine;

namespace IT.WizardBattle.UI
{
    public class GameplaySceneUI : SceneUIBase
    {
        [SerializeField] private UIHealthbarView _healthbar;
        [SerializeField] private UISpellsView _spells;

        private PlayerService _playerService;

        public override void Initialize(SceneBootstrap scene, ApplicationUIContainer appUIContainer)
        {
            base.Initialize(scene, appUIContainer);

            _playerService = scene.GetService<PlayerService>();
            _playerService.OnSpellSelected += OnSpellSelected;
            _playerService.OnPlayerHealthChanged += OnPlayerHealthChanged;

            OnSpellSelected(_playerService.SelectedSpell);
        }

        

        public override void Deinitialize()
        {
            base.Deinitialize();
            _playerService.OnSpellSelected -= OnSpellSelected;
            _playerService.OnPlayerHealthChanged -= OnPlayerHealthChanged;
        }

        private void OnSpellSelected(SpellData spell)
        {
           _spells.SelectSpell(spell);
        }

        private void OnPlayerHealthChanged(float health, float maxHealth)
        {
            _healthbar.SetHealthAmount(health/maxHealth);
        }
    }
}
