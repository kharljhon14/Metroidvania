using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerJump : MonoBehaviour
{
    [field: SerializeField] private UnityEvent OnGrounded { get; set; }

    [SerializeField] private float jumpForce;
    [SerializeField] private float hangTimeMax;

    private GroundDetection groundDetection;
    //Refactor later
    private Rigidbody2D rb2d;
    private float hangTimer;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        groundDetection = GetComponentInChildren<GroundDetection>();
    }

    private void Update()
    {
        OnGrounded?.Invoke();
        groundDetection.IsGrounded();
        GroundedTimer();
    }

    private void GroundedTimer()
    {
        if (groundDetection.IsGrounded())
            hangTimer = hangTimeMax;

        hangTimer -= Time.deltaTime;
    }

    public void Jump()
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
