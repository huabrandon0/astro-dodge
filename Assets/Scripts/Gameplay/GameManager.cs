using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class GameManager : MonoBehaviour
    {
        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent StartSpawningRows;
            public GameEventInt AddToCount;
            public GameEventInt AddToScore;
            public GameEventInt SetScore;
            public GameEventInt SetCount;
            public GameEvent GameEndEvent;
            public GameEvent GameRestartEvent;
        }

        [SerializeField] InvokeEvents _invokeEvents;

        private Coroutine _countCoroutine;

        [SerializeField] private float _timePerCount = 1f;

        private bool _isDead = false;

        [SerializeField] AnimationCurve _endGameSlowmoCurve;
        [SerializeField] float _slowmoTime;
        [SerializeField] float _invokeGameEndEventTime;

        [SerializeField] DifficultyConfig _diffConfig;

        void Start()
        {
            _invokeEvents.GameRestartEvent.Invoke();
        }

        public void StartGame()
        {
            _isDead = false;
            Time.timeScale = 1f;
            _invokeEvents.SetScore.Invoke(0);
            _invokeEvents.SetCount.Invoke(0);
            _countCoroutine = StartCoroutine(Count());
            _invokeEvents.StartSpawningRows.Invoke();
        }

        public void EndGame()
        {
            if (_isDead)
                return;

            _isDead = true;

            if (_countCoroutine != null)
            {
                StopCoroutine(_countCoroutine);
                _countCoroutine = null;
            }

            StartCoroutine(EndGameSlow());
            StartCoroutine(InvokeGameEndEvent());
        }

        IEnumerator InvokeGameEndEvent()
        {
            yield return new WaitForSeconds(_invokeGameEndEventTime);
            _invokeEvents.GameEndEvent.Invoke();
        }

        IEnumerator EndGameSlow()
        {
            float startTimeScale = Time.timeScale;
            float startTime = Time.time - Time.unscaledDeltaTime;
            float unscaledTime = Time.time;
            float interpolationValue = 0f;
            while (interpolationValue <= 1f)
            {
                interpolationValue = (unscaledTime - startTime) / _slowmoTime;
                Time.timeScale = _endGameSlowmoCurve.Evaluate(interpolationValue);
                yield return null;
                unscaledTime += Time.unscaledDeltaTime;
            }
        }

        IEnumerator Count()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timePerCount);
                _invokeEvents.AddToScore.Invoke(_diffConfig.ScorePerCount);
                _invokeEvents.AddToCount.Invoke(_diffConfig.CountInterval);
            }
        }
    }
}
