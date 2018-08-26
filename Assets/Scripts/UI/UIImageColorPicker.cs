using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidRage.UI
{
    [RequireComponent(typeof(Image))]
    public class UIImageColorPicker : MonoBehaviour
    {
        Image _image;

        [SerializeField] Color[] _colors;

        void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetColor(int i)
        {
            if (i < 0 || i >= _colors.Length)
                return;

            _image.color = _colors[i];
        }
    }
}