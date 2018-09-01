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

        public void ExhaustOff()
        {
            foreach (ModelExhaustEffects exhaustEffects in _exhaustEffects)
            {
                if (exhaustEffects.isActiveAndEnabled)
                    exhaustEffects.ExhaustOff();
            }
        }

        public void ExhaustOn()
        {
            foreach (ModelExhaustEffects exhaustEffects in _exhaustEffects)
            {
                if (exhaustEffects.isActiveAndEnabled)
                    exhaustEffects.ExhaustOn();
            }
        }

        public void FlashRed()
        {
            EZCameraShake.CameraShaker.GetInstance("ShipSelectCamera").ShakeOnce(1f, 8f, 0f, 0.75f);

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

        public void MegaExhaustOn()
        {
            foreach (ModelExhaustEffects exhaustEffects in _exhaustEffects)
            {
                if (exhaustEffects.isActiveAndEnabled)
                    exhaustEffects.MegaExhaustOn();
            }
        }

        public void MegaExhaustOff()
        {
            foreach (ModelExhaustEffects exhaustEffects in _exhaustEffects)
            {
                if (exhaustEffects.isActiveAndEnabled)
                    exhaustEffects.MegaExhaustOff();
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
