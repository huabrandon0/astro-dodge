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
    }

    [SerializeField] InvokeEvents _invokeEvents;

    [SerializeField] private float _timePerCount = 1f;

    private Coroutine _scoreCoroutine;

    private bool _isDead = false;

    public void StartGame()
    {
        _scoreCoroutine = StartCoroutine(Count());
        _invokeEvents.StartSpawningRows.Invoke();
    }

    public void EndGame()
    {
        if (_isDead)
            return;

        _isDead = true;

        if (_scoreCoroutine != null)
            StopCoroutine(_scoreCoroutine);
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
