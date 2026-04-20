using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //don't miss that you need this for UnityEvent

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHp = 10;
    [SerializeField] private int hp = 10;

    [SerializeField] private UnityEvent OnDamaged;
    [SerializeField] private UnityEvent OnZero;

    public void Damage(int hpAmount)
    {
        hp -= hpAmount;
        //Debug.Log("hp amount changed by " + hpAmount + " and is now " + hp);

        //tell any subscriber to this event that damage happened!
        OnDamaged?.Invoke();

        //If we hit zero health, raise the OnZero event with all that registered
        if (hp <= 0)
        {
            OnZero?.Invoke();
        }
    }




}