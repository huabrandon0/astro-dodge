using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.UI
{
    /// <summary>
    /// Repesents a UI canvas that can be overlayed the game.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class UIOverlay : MonoBehaviour
    {
        Canvas _canvas;
        IUIFade[] _uiFadeInterfaces;
        bool _enabled = false;

        void Awake()
        {
            // Collect all of the fadable elememts from the overlay.
            _uiFadeInterfaces = GetComponentsInChildren<IUIFade>();
            _canvas = GetComponent<Canvas>();
        }

        public void Show()
        {
            _canvas.enabled = true;
        }

        public void Hide()
        {
            _canvas.enabled = false;
        }

        public void FadeIn()
        {
            _enabled = true;
            gameObject.SetActive(true);
            for (int i = 0; i < _uiFadeInterfaces.Length; ++i)
            {
                _uiFadeInterfaces[i].FadeIn();
            }
        }

        public void FadeOut()
        {
            _enabled = false;
            for (int i = 0; i < _uiFadeInterfaces.Length; ++i)
            {
                _uiFadeInterfaces[i].FadeOut();
            }
        }

        public void FadeInComplete()
        {
            _enabled = true;
            for (int i = 0; i < _uiFadeInterfaces.Length; ++i)
            {
                _uiFadeInterfaces[i].FadeInComplete();
            }
        }

        public void FadeOutComplete()
        {
            _enabled = false;
            for (int i = 0; i < _uiFadeInterfaces.Length; ++i)
            {
                _uiFadeInterfaces[i].FadeOutComplete();
            }
        }

        public void ToggleFade()
        {
            if (!_enabled)
            {
                _enabled = true;
                foreach (IUIFade uiFade in _uiFadeInterfaces)
                    uiFade.FadeIn();
            }
            else
            {
                _enabled = false;
                foreach (IUIFade uiFade in _uiFadeInterfaces)
                    uiFade.FadeOut();
            }
        }
    }
}
