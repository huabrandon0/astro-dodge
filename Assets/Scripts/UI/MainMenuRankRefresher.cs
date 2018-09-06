using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;
using AsteroidRage.Extensions;
using TMPro;
using UnityEngine.UI;

namespace AsteroidRage.UI
{
    public class MainMenuRankRefresher : MonoBehaviour
    {
        [SerializeField] GameEventInt _updateInt;
        
        [SerializeField] Image _symbol;
        [SerializeField] TextMeshProUGUI _text;
        
        [System.Serializable]
        class Rank
        {
            public Color color;
            public int threshold;
        }

        [SerializeField] Rank[] _ranks;

		void OnEnable()
		{
            if (_updateInt)
                _updateInt.AddListener(RefreshInt);
		}

		void OnDisable()
        {
            if (_updateInt)
                _updateInt.RemoveListener(RefreshInt);
        }

        public void RefreshInt(int score)
        {
            _text.SetText(score.ToString());

            for (int i = 0; i < _ranks.Length; i++)
            {
                if (score < _ranks[i].threshold)
                {
                    _symbol.color = _ranks[i].color;
                    _text.color = _ranks[i].color;
                    return;
                }
            }

            _symbol.color = _ranks[_ranks.Length - 1].color;
            _text.color = _ranks[_ranks.Length - 1].color;
        }
    }
}
