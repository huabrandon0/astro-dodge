using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using AsteroidRage.Extensions;

namespace AsteroidRage.UI
{
    /// <summary>
    /// Fade in and out UI Text fields.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class UITextFade : MonoBehaviour, IUIFade
    {
        [SerializeField] float _fadeTime = 0f;
        [SerializeField] float _blinkTime = 0f;

        Text _text;

        [SerializeField] bool _blinking = false;

        Sequence _mySequence;

        void Awake()
        {
            _text = this.GetComponentAssert<Text>();
        }

        void Start()
        {
            if (_blinking)
            {
                _text.DOFade(1f, _blinkTime).SetLoops(-1, LoopType.Yoyo);
            }
        }

        public void FadeIn()
        {
            _text.DOFade(1f, _fadeTime);
        }

        public void FadeOut()
        {
            _text.DOFade(0f, _fadeTime);
        }

        public void FadeInComplete()
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0f);
            _text.DOFade(1f, _fadeTime);
        }

        public void FadeOutComplete()
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1f);
            _text.DOFade(0f, _fadeTime);
        }
    }
}
