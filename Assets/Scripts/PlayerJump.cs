using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerJump : Abilities
{
    [field: SerializeField] private UnityEvent OnGrounded { get; set; }

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
        OnGrounded?.Invoke();
        groundDetection.IsGrounded();
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
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void SmallJump()
    {
        if (rigidbody2D.velocity.y > 0f)
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * .4f);
    }
}
