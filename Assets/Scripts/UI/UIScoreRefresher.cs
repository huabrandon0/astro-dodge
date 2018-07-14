using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;
using AsteroidRage.Extensions;
using AsteroidRage.Game;
using UnityEngine.UI;

namespace AsteroidRage.UI
{
    /// <summary>
    /// Refreshes the attached Text component when the score is updated.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class UIScoreRefresher : MonoBehaviour
    {
        [SerializeField] GameEvent _updateScore;

        Text _scoreText;

        void Awake()
        {
            _scoreText = this.GetComponentAssert<Text>();
        }

		void OnEnable()
		{
            _updateScore.AddListener(RefreshScore);
		}
		void OnDisable()
        {
            _updateScore.RemoveListener(RefreshScore);
        }

        void RefreshScore()
        {
            _scoreText.text = ScoreManager.Instance.Score.ToString();
        }
    }
}
