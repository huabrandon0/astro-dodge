using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.Game
{
    public class AsteroidModel : MonoBehaviour
    {   
        [SerializeField] GameObject[] _asteroidModels;
        MeshRenderer _mr;
        Animator _anim;

        [SerializeField] Color _onColor;
        [SerializeField] Color _offColor;
        
        [SerializeField] float _fadeInTime;

        void Awake()
        {
            GameObject model = Instantiate(_asteroidModels[Random.Range(0, _asteroidModels.Length)], transform);
            _mr = model.GetComponentInChildren<MeshRenderer>();
            _anim = GetComponent<Animator>();
        }

        public void On()
        {
            OnColorFade();
            _anim.SetTrigger("PopUp");
        }

        public void Off()
        {
            _mr.material.color = new Color(_mr.material.color.r, _mr.material.color.g, _mr.material.color.b, _offColor.a);
        }

        public void OnColorFade()
        {
            _mr.material.color = new Color(_mr.material.color.r, _mr.material.color.g, _mr.material.color.b, 0f);
            StartCoroutine(ColorLerp(_onColor, _fadeInTime));
        }

        IEnumerator ColorLerp(Color col, float fadeTime)
        {
            Color startColor = _mr.material.GetColor("_Color");
            Color endCol = new Color(startColor.r, startColor.g, startColor.b, col.a);
            float startTime = Time.time - Time.deltaTime;
            float interpolationValue = 0f;
            while (interpolationValue <= 1f)
            {
                interpolationValue = (Time.time - startTime) / fadeTime;
                _mr.material.SetColor("_Color", Color.Lerp(startColor, endCol, interpolationValue));
                yield return null;
            }
        }
    }
}
