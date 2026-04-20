using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private int score = 0;

    public void Start()
    {
        score = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log($"At start {score} enemies total.");
    }

    public void ReduceScore()
    {
        score--;
        Debug.Log($"{score} enemies remaining");

        if (score <= 0)
        {
            GameEventDispatcher.TriggerEnemiesAllDefeated();
        }
    }

    private void OnEnable()
    {
        GameEventDispatcher.EnemyDefeated += ReduceScore;
    }

    private void OnDisable()
    {
        GameEventDispatcher.EnemyDefeated -= ReduceScore;
    }


}