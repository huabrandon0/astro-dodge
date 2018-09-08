using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TWM.UI
{
    [RequireComponent(typeof(Animator))]
    public class UIElementAnimator : MonoBehaviour
    {
        Animator _anim;
        List<Animator> _anims;

        [SerializeField] bool _blink;

        [SerializeField] float _enterDelay;
        [SerializeField] float _exitDelay;

        void Awake()
        {
            _anim = GetComponent<Animator>();
            _anims = GetComponentsInChildren<Animator>().ToList();
        }

        void Start()
        {
            foreach (Animator anim in _anims)
                anim.SetBool("Blinking", _blink);
        }
        
        public void Enter()
        {
            StartCoroutine(Enter(_enterDelay));
        }

        IEnumerator Enter(float time)
        {
            yield return new WaitForSeconds(time);
            FadeIn();
            BounceUp();
            ScaleIdle();
        }

        public void Exit()
        {
            StartCoroutine(Exit(_exitDelay));
        }

        IEnumerator Exit(float time)
        {
            yield return new WaitForSeconds(time);
            FadeOut();
            SlideDown();
            ScaleIdle();
        }

        public void FadeIn()
        {
            foreach (Animator anim in _anims)
            {
                if (anim.isActiveAndEnabled)
                    anim.Play("AlphaFadeIn", -1, 0);
            }
        }

        public void FadeOut()
        {
            foreach (Animator anim in _anims)
            {
                if (anim.isActiveAndEnabled)
                    anim.Play("AlphaFadeOut", -1, 0);
            }
        }

        public void ScaleUp()
        {
            _anim.Play("Scale0To1", -1, 0);
        }

        public void ScaleDown()
        {
            _anim.Play("Scale1To0", -1, 0);
        }

        public void ScaleIdle()
        {
            _anim.Play("Scale1", -1, 0);
        }

        public void SlideUp()
        {
            _anim.Play("PositionSlideUp", -1, 0);
        }

        public void SlideDown()
        {
            _anim.Play("PositionSlideDown", -1, 0);
        }

        public void BounceUp()
        {
            _anim.Play("PositionBounceUp", -1, 0);
        }

        public void PushOut()
        {
            _anim.Play("ScalePushOut", -1, 0);
        }

        public void PushIn()
        {
            _anim.Play("ScalePushIn", -1, 0);
        }
    }
}
