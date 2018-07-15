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
        public GameEvent IncreaseScore;
    }

    [SerializeField] InvokeEvents _invokeEvents;

    [SerializeField] private float timeBetweenIncrements = 1f;

    private Coroutine scoreCoroutine;

    private bool isDead = false;

    public void StartGame()
    {
        this.scoreCoroutine = StartCoroutine(IncreaseScore());
        _invokeEvents.StartSpawningRows.Invoke();
    }

    public void EndGame()
    {
        if (this.isDead)
            return;

        this.isDead = true;

        if (this.scoreCoroutine != null)
            StopCoroutine(this.scoreCoroutine);
    }
    
    private IEnumerator IncreaseScore()
    {
        while (true)
        {
            _invokeEvents.IncreaseScore.Invoke();
            yield return new WaitForSeconds(this.timeBetweenIncrements);
        }
    }
}
