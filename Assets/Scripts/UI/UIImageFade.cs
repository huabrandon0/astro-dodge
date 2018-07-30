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
    [RequireComponent(typeof(Image))]
    public class UIImageFade : MonoBehaviour, IUIFade
    {
        [SerializeField] float _fadeTime = 0;

        Image _image;

        void Awake()
        {
            _image = this.GetComponentAssert<Image>();
        }

        public void FadeIn()
        {
            _image.DOFade(1f, _fadeTime);
        }

        public void FadeOut()
        {
            _image.DOFade(0f, _fadeTime);
        }

        public void FadeInComplete()
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
            _image.DOFade(1f, _fadeTime);
        }

        public void FadeOutComplete()
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
            _image.DOFade(0f, _fadeTime);
        }
    }
}
