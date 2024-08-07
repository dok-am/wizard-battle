using IT.CoreLib.Interfaces;
using IT.WizardBattle.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IT.WizardBattle.Services
{
    public class VFXService : IService
    {

        private List<VFX> _effectsPool = new();
        private Transform _vfxContainer;


        private const int MAX_EFFECTS_COUNT = 20;


        public void OnInitialized(IContext context) 
        {
            _vfxContainer = new GameObject("VFX_POOL").transform;
        }

        public void PlayVisualEffect(VFX effectPrefab, Vector2 position)
        {
            VFX instance = TryGetEffectFromPool(effectPrefab.Id);
            if (instance == null)
            {
                instance = AddEffectToPool(effectPrefab);
            }

            instance.Play(position);
        }


        private VFX TryGetEffectFromPool(string effectId)
        {
            foreach (VFX effect in _effectsPool)
            {
                if (effect.Id.Equals(effectId) && !effect.Available)
                    return effect;
            }

            return null;
        }

        private VFX AddEffectToPool(VFX effectPrefab)
        {
            VFX instance = GameObject.Instantiate(effectPrefab, _vfxContainer);

            if (instance == null)
                throw new Exception("[VFX] Can't spawn vfx: prefab is incorrect!");

           RemoveOldestEffectIfNeeded();

            _effectsPool.Add(instance);

            return instance;
        }

        private void RemoveOldestEffectIfNeeded()
        {
            if (_effectsPool.Count < MAX_EFFECTS_COUNT)
                return;

            VFX effect = _effectsPool[0];
            _effectsPool.RemoveAt(0);

            GameObject.Destroy(effect.gameObject);
        }
    }
}