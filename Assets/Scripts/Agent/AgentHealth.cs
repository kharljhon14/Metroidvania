using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentHealth : MonoBehaviour, IHitable
{
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [field: SerializeField] public UnityEvent OnGetDie { get; set; }

    [SerializeField] protected int maxHealth;

    [HideInInspector] public int currentHealth;
    private bool isDead = false;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!isDead)
        {
            OnGetHit?.Invoke();
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                OnGetDie?.Invoke();
            }
        }
    }
}
