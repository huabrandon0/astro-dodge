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

        [SerializeField] float _animatorSpeed = 1f;

        [SerializeField] bool _blink;

        [SerializeField] bool _enterFadeIn = true;
        [SerializeField] bool _enterBounceUp = true;
        [SerializeField] bool _enterSlideUp = false;
        [SerializeField] bool _enterScaleUp = false;
        [SerializeField] bool _exitFadeOut = true;
        [SerializeField] bool _exitBounceDown = true;
        [SerializeField] bool _exitSlideDown = false;
        [SerializeField] bool _exitScaleDown = false;

        [SerializeField] float _enterDelay;
        [SerializeField] float _exitDelay;

        void Awake()
        {
            _anim = GetComponent<Animator>();
            _anims = GetComponentsInChildren<Animator>().ToList();
            foreach (Animator anim in _anims)
            {
                anim.speed = _animatorSpeed;
                anim.SetBool("Blinking", _blink);
            }
        }

        public void IdleOff()
        {
            Alpha0();
            PositionOffscreen();
            Scale0();
        }

        public void IdleOn()
        {
            Alpha1();
            Position0();
            Scale1();
        }

        public void Enter()
        {
            StartCoroutine(Enter(_enterDelay));
        }

        IEnumerator Enter(float time)
        {
            yield return new WaitForSeconds(time);

            if (_enterFadeIn)
                FadeIn();
            else
                Alpha1();

            if (_enterBounceUp)
                BounceUp();
            else if (_enterSlideUp)
                SlideUp();
            else
                Position0();

            if (_enterScaleUp)
                ScaleUp();
            else
                Scale1();
        }

        public void Exit()
        {
            StartCoroutine(Exit(_exitDelay));
        }

        IEnumerator Exit(float time)
        {
            yield return new WaitForSeconds(time);

            if (_exitFadeOut)
                FadeOut();
            else
                Alpha1();

            if (_exitBounceDown)
                BounceDown();
            else if (_exitSlideDown)
                SlideDown();
            else
                Position0();

            if (_exitScaleDown)
                ScaleDown();
            else
                Scale1();
        }

        public void Alpha1()
        {
            foreach (Animator anim in _anims)
            {
                if (_anim.isActiveAndEnabled)
                    _anim.Play("Alpha1", -1, 0);
            }
        }

        public void Alpha0()
        {
            foreach (Animator anim in _anims)
            {
                if (_anim.isActiveAndEnabled)
                    _anim.Play("Alpha0", -1, 0);
            }
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
        
        public void Scale1()
        {
            _anim.Play("Scale1", -1, 0);
        }

        public void Scale0()
        {
            _anim.Play("Scale0", -1, 0);
        }

        public void ScaleUp()
        {
            _anim.Play("Scale0To1", -1, 0);
        }

        public void ScaleDown()
        {
            _anim.Play("Scale1To0", -1, 0);
        }

        public void Position0()
        {
            _anim.Play("Position0", -1, 0);
        }

        public void PositionOffscreen()
        {
            _anim.Play("PositionOffscreen", -1, 0);
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

        public void BounceDown()
        {
            _anim.Play("PositionBounceDown", -1, 0);
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
