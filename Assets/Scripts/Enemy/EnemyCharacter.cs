using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCharacter : Enemy, IHitable
{
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [field: SerializeField] public UnityEvent OnGetDie { get; set; }

    [SerializeField] protected int maxHealth;

    private int currentHealth;
    private bool isDead = false;
    

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!isDead)
        {
            Debug.Log("HIT");
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                OnGetDie?.Invoke();
                Destroy(gameObject, 1f);
            }
        }
    }
}
