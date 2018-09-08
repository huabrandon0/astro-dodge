using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TWM.UI
{
    public class UIAnimatorOverlay : MonoBehaviour
    {
        List<Animator> _anims;

        void Awake()
        {
            _anims = GetComponentsInChildren<Animator>().ToList();
            foreach (Animator anim in _anims)
            {
                if (anim.runtimeAnimatorController.name != "Image")
                    _anims.Remove(anim);
            }
        }

        public void Enter()
        {
            foreach (Animator anim in _anims)
            {
                anim.Play("ImageAlphaFadeIn", -1, 0);
                anim.Play("ImageScale0To1", -1, 0);
                anim.Play("ImagePositionSlideUp", -1, 0);
            }
        }

        public void Exit()
        {

        }
    }
}
