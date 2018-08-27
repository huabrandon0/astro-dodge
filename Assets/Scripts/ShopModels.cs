using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.Game
{
    public class ShopModels : MonoBehaviour
    {
        FlashRed[] _flashReds;
        ExhaustEffects[] _exhaustEffects;

        void Awake()
        {
            _flashReds = GetComponentsInChildren<FlashRed>();
            _exhaustEffects = GetComponentsInChildren<ExhaustEffects>();
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
            foreach (ExhaustEffects exhaustEffects in _exhaustEffects)
            {
                if (exhaustEffects.isActiveAndEnabled)
                    exhaustEffects.Flare();
            }
        }

        public void MegaFlareOn()
        {
            foreach (ExhaustEffects exhaustEffects in _exhaustEffects)
            {
                if (exhaustEffects.isActiveAndEnabled)
                    exhaustEffects.MegaFlareOn();
            }
        }

        public void MegaFlareOff()
        {
            foreach (ExhaustEffects exhaustEffects in _exhaustEffects)
            {
                if (exhaustEffects.isActiveAndEnabled)
                    exhaustEffects.MegaFlareOff();
            }
        }
    }
}
