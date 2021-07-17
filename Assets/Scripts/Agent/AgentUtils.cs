using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentUtils : MonoBehaviour
{
    private AgentMovement agentMovement;
    private bool isKnockback = false;

    private void Start()
    {
        agentMovement = GetComponent<AgentMovement>();
    }

    public void Knockback(Vector2 direction, float power, float duration)
    {
        if (!isKnockback)
        {
            isKnockback = true;
            StartCoroutine(KnockbackCoroutine(direction, power, duration));
        }
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction, float power, float duration)
    {
        agentMovement.rb2d.AddForce(direction.normalized * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        ResetKnockbackValues();
    }

    private void ResetKnockbackValues()
    {
        isKnockback = false;
        agentMovement.currentVelocity = 0;
        agentMovement.rb2d.velocity = Vector2.zero;
    }
}
