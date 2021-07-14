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

    private void FixedUpdate()
    {
        OnVelocityChanged?.Invoke(currentVelocity);
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
}
