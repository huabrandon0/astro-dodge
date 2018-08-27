using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AsteroidRage.UI
{
    [RequireComponent(typeof(TextMeshProUGUI), typeof(Animator))]
    public class TextMeshProTextWrapper : MonoBehaviour
    {
        TextMeshProUGUI _text;
        Animator _anim;

        [SerializeField] bool _blink = false;
        
        void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _anim = GetComponent<Animator>();

        }

        void Start()
        {
            _anim.SetBool("Blinking", _blink);
        }
    }
}
