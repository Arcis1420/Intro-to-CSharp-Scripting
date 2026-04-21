using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHp = 10;
    [SerializeField] private int hp = 10;

    [SerializeField] private UnityEvent OnDamaged;
    [SerializeField] private UnityEvent OnZero;

    public void Damage(int hpAmount)
    {
        hp -= hpAmount;

        OnDamaged?.Invoke();

        if (hp <= 0)
        {
            OnZero?.Invoke();
        }
    }




}