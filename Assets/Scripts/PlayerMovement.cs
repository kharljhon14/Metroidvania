using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMovement : Abilities
{
    [field: SerializeField] private UnityEvent<float> OnVelocityChanged { get; set; }

    [SerializeField] private float timeTillMaxSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

    private float currentSpeed;
    private float runTime;

    private void FixedUpdate()
    {
        OnVelocityChanged?.Invoke(currentSpeed);
        rb2d.velocity = new Vector2(currentSpeed, rb2d.velocity.y);
    }

    public void MovePlayer(float horizontalInput)
    {
        if (horizontalInput != 0)
        {
            acceleration = maxSpeed / timeTillMaxSpeed;
            runTime += Time.deltaTime;
            currentSpeed = horizontalInput * acceleration * runTime;
            CheckSpeed();
        }

        else
        {
            acceleration = 0;
            runTime = 0;
            currentSpeed = 0;
        }
    }


    private void CheckSpeed()
    {
        if (currentSpeed > maxSpeed)
            currentSpeed = maxSpeed;

        else if (currentSpeed < -maxSpeed)
            currentSpeed = -maxSpeed;
    }
}
