using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerJump : Abilities
{
    [field: SerializeField] private UnityEvent<bool> OnGrounded { get; set; }
    [field: SerializeField] private UnityEvent<float> OnVelocityYChanged { get; set; }

    [SerializeField] private float jumpForce;
    [SerializeField] private float hangTimeMax;
    [SerializeField] private float jumpBufferMax;

    private GroundDetection groundDetection;
    private float hangTimer;
    private float jumpBufferTimer;

    private void Start()
    {
        groundDetection = GetComponentInChildren<GroundDetection>();
    }

    private void Update()
    {
        OnGrounded?.Invoke(groundDetection.IsGrounded());

        OnVelocityYChanged?.Invoke(rb2d.velocity.y);

        GroundedTimer();
        jumpBufferTimer -= Time.deltaTime;
    }

    public void ResetJumpTimer()
    {
        jumpBufferTimer = jumpBufferMax;
        if (jumpBufferTimer > 0)
            Jump();
    }

    private void GroundedTimer()
    {
        if (groundDetection.IsGrounded())
            hangTimer = hangTimeMax;

        hangTimer -= Time.deltaTime;
    }

    private void Jump()
    {
        if (hangTimer > 0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void SmallJump()
    {
        if (rb2d.velocity.y > 0f)
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * .4f);
    }
}
