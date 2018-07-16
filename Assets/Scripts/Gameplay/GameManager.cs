using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using AsteroidRage.Events;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class InvokeEvents
    {
        public GameEvent StartSpawningRows;
        public GameEventInt AddToCount;
        public GameEventInt AddToScore;
        public GameEvent GameEndEvent;
    }

    [SerializeField] InvokeEvents _invokeEvents;

    private Coroutine _countCoroutine;

    [SerializeField] private float _timePerCount = 1f;

    private bool _isDead = false;

    [SerializeField] AnimationCurve _endGameSlowmoCurve;
    [SerializeField] float _slowmoTime;

    public void StartGame()
    {
        Time.timeScale = 1f;
        _countCoroutine = StartCoroutine(Count());
        _invokeEvents.StartSpawningRows.Invoke();
    }

    public void EndGame()
    {
        if (_isDead)
            return;

        _isDead = true;

        if (_countCoroutine != null)
            StopCoroutine(_countCoroutine);

        StartCoroutine(EndGameSlow());
    }

    private IEnumerator EndGameSlow()
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

        _invokeEvents.GameEndEvent.Invoke();
    }
    
    private IEnumerator Count()
    {
        while (true)
        {
            _invokeEvents.AddToScore.Invoke(5);
            _invokeEvents.AddToCount.Invoke(1);
            yield return new WaitForSeconds(_timePerCount);
        }
    }
}
