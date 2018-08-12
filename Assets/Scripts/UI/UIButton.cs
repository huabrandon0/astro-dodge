using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using AsteroidRage.Extensions;
using UnityEngine.Events;

namespace AsteroidRage.UI
{
    [RequireComponent(typeof(Image))]
    public class UIButton : MonoBehaviour
    {
        Image _image;

        [SerializeField] Sprite _offSprite;
        [SerializeField] Sprite _onSprite;

        [SerializeField] UnityEvent _onTurnedOn;
        [SerializeField] UnityEvent _onTurnedOff;

        bool _on = true;

        void Awake()
        {
            _image = this.GetComponentAssert<Image>();
            On();
        }

        public void Toggle()
        {
            if (!_on)
            {
                On();
            }
            else
            {
                Off();
            }
        }

        public void On()
        {
            _on = true;
            _image.sprite = _onSprite;
            _onTurnedOn.Invoke();
        }

        public void Off()
        {
            _on = false;
            _image.sprite = _offSprite;
            _onTurnedOff.Invoke();
        }
    }
}
