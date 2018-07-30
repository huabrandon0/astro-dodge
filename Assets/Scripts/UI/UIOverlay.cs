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
            gameObject.SetActive(true);
            for (int i = 0; i < _uiFadeInterfaces.Length; ++i)
            {
                _uiFadeInterfaces[i].FadeIn();
            }
        }

        public void FadeOut()
        {
            for (int i = 0; i < _uiFadeInterfaces.Length; ++i)
            {
                _uiFadeInterfaces[i].FadeOut();
            }
        }

        public void FadeInComplete()
        {
            for (int i = 0; i < _uiFadeInterfaces.Length; ++i)
            {
                _uiFadeInterfaces[i].FadeInComplete();
            }
        }

        public void FadeOutComplete()
        {
            for (int i = 0; i < _uiFadeInterfaces.Length; ++i)
            {
                _uiFadeInterfaces[i].FadeOutComplete();
            }
        }
    }
}
