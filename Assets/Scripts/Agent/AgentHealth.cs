using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentHealth : MonoBehaviour, IHitable, IKnockbackable
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
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Debug.Log("Dead");
                currentHealth = 0;
                isDead = true;
                OnGetDie?.Invoke();
            }
        }
    }

    public void Knockback(Vector2 direction, float power, float duration)
    {
        
    }
}
