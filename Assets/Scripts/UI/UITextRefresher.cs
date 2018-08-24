using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;
using AsteroidRage.Extensions;
using TMPro;

namespace AsteroidRage.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UITextRefresher : MonoBehaviour
    {
        [SerializeField] GameEventInt _updateText;

        TextMeshProUGUI _text;

        [SerializeField] string _prefix = "";

        void Awake()
        {
            _text = this.GetComponentAssert<TextMeshProUGUI>();
        }

		void OnEnable()
		{
            _updateText.AddListener(RefreshScore);
		}

		void OnDisable()
        {
            _updateText.RemoveListener(RefreshScore);
        }

        void RefreshScore(int score)
        {
            _text.SetText(_prefix + score.ToString());
        }
    }
}
