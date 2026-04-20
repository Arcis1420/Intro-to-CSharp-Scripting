using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Celebrate()
    {
        _particleSystem.Play();
    }

    void OnEnable()
    {
        GameEventDispatcher.EnemiesAllDefeated += Celebrate;
    }

    void OnDisable()
    {
        GameEventDispatcher.EnemiesAllDefeated -= Celebrate;
    }
}