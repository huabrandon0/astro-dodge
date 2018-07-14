using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using AsteroidRage.Game;

public class GameManager : Singleton<GameManager>
{
    protected GameManager() { }

    [SerializeField] private float timeBetweenIncrements = 1f;

    private Coroutine scoreCoroutine;

    private bool isDead = false;

    public void StartGame()
    {
        ScoreManager.Instance.ResetScore();
        this.scoreCoroutine = StartCoroutine(IncreaseScore());
        StartCoroutine(RowSpawner.Instance.SpawnRowsContinuously());
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
            ScoreManager.Instance.IncrementScore();
            yield return new WaitForSeconds(this.timeBetweenIncrements);
        }
    }
}
