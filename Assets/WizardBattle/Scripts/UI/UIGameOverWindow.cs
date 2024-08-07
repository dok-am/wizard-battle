using IT.CoreLib.Application;
using IT.CoreLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace IT.WizardBattle
{
    public class UIGameOverWindow : UIWindowBase
    {
        [SerializeField] private Button _restartButton;

        private SceneContext _scene;


        public override void Initialize(SceneContext scene)
        {
            _scene = scene;
            _restartButton.onClick.AddListener(OnRestartClicked);
        }


        private void OnRestartClicked()
        {
            _restartButton.enabled = false;
            _scene.ReloadScene();
        }
    }
}
