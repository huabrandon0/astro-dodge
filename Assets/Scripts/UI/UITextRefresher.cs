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
        [SerializeField] GameEventInt _updateInt;
        [SerializeField] GameEventString _updateString;

        TextMeshProUGUI _text;

        [SerializeField] string _prefix = "";
        [SerializeField] string _suffix = "";

        void Awake()
        {
            _text = this.GetComponentAssert<TextMeshProUGUI>();
        }

		void OnEnable()
		{
            if (_updateInt)
                _updateInt.AddListener(RefreshInt);

            if (_updateString)
                _updateString.AddListener(RefreshText);
		}

		void OnDisable()
        {
            if (_updateInt)
                _updateInt.RemoveListener(RefreshInt);
            if (_updateString)
                _updateString.RemoveListener(RefreshText);
        }

        public void RefreshInt(int score)
        {
            _text.SetText(_prefix + score.ToString() + _suffix);
        }

        public void RefreshText(string text)
        {
            _text.SetText(_prefix + text + _suffix);
        }
    }
}
