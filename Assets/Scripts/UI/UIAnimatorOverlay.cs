using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.UI
{
    public class UIAnimatorOverlay : MonoBehaviour
    {
        Animator[] _anims;

        void Awake()
        {
            _anims = GetComponentsInChildren<Animator>();
        }

        public void FadeIn()
        {
            foreach (Animator anim in _anims)
            {
                if (anim.isActiveAndEnabled)
                    anim.SetTrigger("FadeIn");
            }
        }

        public void FadeOut()
        {
            foreach (Animator anim in _anims)
            {
                if (anim.isActiveAndEnabled)
                    anim.SetTrigger("FadeOut");
            }
        }
    }
}
