using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventDispatcher
{
    public delegate void GameEventHandler();

    public static event GameEventHandler EnemyDefeated;
    public static event GameEventHandler EnemiesAllDefeated;

    public static void TriggerEnemyDefeated()
    {
        EnemyDefeated?.Invoke();
    }

    public static void TriggerEnemiesAllDefeated()
    {
        EnemiesAllDefeated?.Invoke();
    }
}
