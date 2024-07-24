using IT.CoreLib.Application;
using IT.CoreLib.UI;
using UnityEngine;

namespace IT.WizardBattle.UI
{
    public class GameplaySceneUI : SceneUIBase
    {
        [SerializeField] private UIHealthbarView _healthbar;

        public override void Initialize(SceneBootstrap scene, ApplicationUIContainer appUIContainer)
        {
            base.Initialize(scene, appUIContainer);

        }
    }
}
