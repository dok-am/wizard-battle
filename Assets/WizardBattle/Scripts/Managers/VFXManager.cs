using IT.CoreLib.Interfaces;
using IT.CoreLib.Scripts;
using IT.WizardBattle.Game;
using UnityEngine;

namespace IT.WizardBattle.Managers
{
    public class VFXManager : TypedPoolSpawnManager<VFX, VFX>
    {
        public VFXManager()
        {
            _containerName = "VFX_POOL";
            _maxCount = 20;

            Initialize();
        }

        public void PlayVisualEffect(VFX effectPrefab, Vector2 position)
        {
            
            VFX instance = Spawn(effectPrefab, effectPrefab);

            if (instance != null)
                instance.Play(position);
        }

        protected override void OnObjectCreated(VFX obj, VFX config)
        {
            //do nothing
        }
    }
}