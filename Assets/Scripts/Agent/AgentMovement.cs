using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AgentMovement : Abilities
{
    [field: SerializeField] private UnityEvent<float> OnVelocityChanged { get; set; }
    [SerializeField] private MovementStats movementStats;

    [HideInInspector] public float currentVelocity;
    private float direction;
    private bool isKnockback = false;

    private void FixedUpdate()
    {
        OnVelocityChanged?.Invoke(currentVelocity);

        if (!isKnockback)
            rb2d.velocity = new Vector2(currentVelocity * direction, rb2d.velocity.y);
    }

    public void MovePlayer(float horizontalInput)
    {
        if (horizontalInput != 0)
        {
            if (horizontalInput != direction)
                currentVelocity = 0f;

            direction = horizontalInput;
        }

        currentVelocity = CheckSpeed(horizontalInput);
    }

    private float CheckSpeed(float horizontalInput)
    {
        if (Mathf.Abs(horizontalInput) > 0)
            currentVelocity += movementStats.acceleration * Time.deltaTime;
        else
            currentVelocity -= movementStats.deceleration * Time.deltaTime;

        return Mathf.Clamp(currentVelocity, 0, movementStats.maxSpeed);
    }

    public void StartKnockback(Vector2 direction, float power, float duration)
    {
        if (!isKnockback)
        {
            isKnockback = true;
            StartCoroutine(KnockbackCoroutine(direction, power, duration));
        }
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction, float power, float duration)
    {
        rb2d.AddForce(direction.normalized * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        ResetKnockbackValues();
    }

    private void ResetKnockbackValues()
    {
        isKnockback = false;
        currentVelocity = 0;
        rb2d.velocity = Vector2.zero;
    }
}