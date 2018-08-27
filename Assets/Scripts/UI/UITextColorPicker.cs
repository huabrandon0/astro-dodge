using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AsteroidRage.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UITextColorPicker : MonoBehaviour
    {
        TextMeshProUGUI _text;

        [SerializeField] Color[] _colors;

        void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void SetColor(int i)
        {
            if (i < 0 || i >= _colors.Length)
                return;

            _text.color = _colors[i];
        }
    }
}
