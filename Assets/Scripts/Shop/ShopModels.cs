using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.Game
{
    public class ShopModels : MonoBehaviour
    {
        FlashRed[] _flashReds;
        ModelExhaustEffects[] _exhaustEffects;
        Animator[] _anims;

        void Awake()
        {
            _flashReds = GetComponentsInChildren<FlashRed>();
            _exhaustEffects = GetComponentsInChildren<ModelExhaustEffects>();
            _anims = GetComponentsInChildren<Animator>();
        }

        public void FlashRed()
        {
            foreach (FlashRed flashRed in _flashReds)
            {
                if (flashRed.isActiveAndEnabled)
                    flashRed.Flash();
            }
        }

        public void Flare()
        {
            foreach (ModelExhaustEffects exhaustEffects in _exhaustEffects)
            {
                if (exhaustEffects.isActiveAndEnabled)
                    exhaustEffects.Flare();
            }
        }

        public void MegaFlareOn()
        {
            Flare();

            foreach (ModelExhaustEffects exhaustEffects in _exhaustEffects)
            {
                if (exhaustEffects.isActiveAndEnabled)
                    exhaustEffects.MegaFlareOn();
            }
        }

        public void MegaFlareOff()
        {
            foreach (ModelExhaustEffects exhaustEffects in _exhaustEffects)
            {
                if (exhaustEffects.isActiveAndEnabled)
                    exhaustEffects.MegaFlareOff();
            }
        }

        public void SetTrigger(string trigger)
        {
            foreach (Animator anim in _anims)
            {
                if (anim.isActiveAndEnabled)
                    anim.SetTrigger(trigger);
            }
        }
    }
}
