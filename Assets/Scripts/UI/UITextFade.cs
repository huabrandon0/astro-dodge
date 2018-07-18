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
        [SerializeField] float _fadeTime = 0;

        Text _text;

        void Awake()
        {
            _text = this.GetComponentAssert<Text>();
        }

        public void FadeIn()
        {
            _text.DOFade(1f, _fadeTime);
        }

        public void FadeOut()
        {
            _text.DOFade(0f, _fadeTime);
        }
    }
}
