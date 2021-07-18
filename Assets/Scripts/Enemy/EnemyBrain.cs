using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : Enemy
{
    [field: SerializeField] private UnityEvent<float> OnMoveInput { get; set; }
    [field: SerializeField] private UnityEvent<float> OnVelocityChanged { get; set; }
    [field: SerializeField] private UnityEvent<bool> OnPlayerDetected { get; set; }
    [SerializeField] private MovementStats movementStats;

    private float currentVelocity;

    [HideInInspector] public bool canChasePlayer;
    [HideInInspector] public bool isFacingLeft;

    [Header("Enemy Flags")]
    [SerializeField] private bool spawnFacingLeft;
    [SerializeField] private bool canTurnAroundCollider;
    [SerializeField] private bool canTurnAroundEdge;
    [SerializeField] private bool canJump;
    [SerializeField] private bool standStill;


    [Header("Timers")]
    [SerializeField] private float originalWaitTime;
    [SerializeField] private float originalTimeTillDoAction;

    [SerializeField] LayerMask collision;
    [SerializeField] private float minDistance;

    protected Transform player;
    protected AgentMovement agentMovement;

    private float moveDirection = 1f;
    private float currentTimer;
    private float timeTillDoAction;
    private bool isWaiting;
    private bool isJumping;
    private bool tooClose;

    private void Start()
    {
        if (spawnFacingLeft)
            isFacingLeft = true;

        player = FindObjectOfType<Character>().transform;
        currentTimer = originalWaitTime;
        timeTillDoAction = originalTimeTillDoAction;
        agentMovement = GetComponent<AgentMovement>();
    }

    private void Update()
    {
        OnMoveInput?.Invoke(moveDirection);
        OnPlayerDetected?.Invoke(canChasePlayer);
        CheckFloorEdge();
        CheckEdge(collision);
        HandleWait();
        FollowPlayer();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        if (!standStill)
        {
            OnVelocityChanged?.Invoke(currentVelocity);
            rb2d.velocity = new Vector2(currentVelocity * moveDirection, rb2d.velocity.y);


        }
    }

    private void Move()
    {
        float distance = .5f;
        if (isFacingLeft)
        {
            moveDirection = -1f;
            if (CollisionCheck(Vector2.left, distance, collision) && canTurnAroundCollider && !isJumping || (canChasePlayer && player.transform.position.x > transform.position.x))
                isFacingLeft = false;
        }

        else
        {
            moveDirection = 1f;
            if (CollisionCheck(Vector2.right, distance, collision) && canTurnAroundCollider && !isJumping || (canChasePlayer && player.transform.position.x < transform.position.x))
                isFacingLeft = true;
        }
    }

    private void Jump()
    {
        if (rayHitNumber > 0 && canJump)
        {
            timeTillDoAction -= Time.deltaTime;
            if (timeTillDoAction <= 0f)
            {
                isJumping = true;
                AddForceVelocityY();

                timeTillDoAction = originalTimeTillDoAction;
                if (isFacingLeft)
                    rb2d.velocity = new Vector2(-5f, rb2d.velocity.y);
                else
                    rb2d.velocity = new Vector2(5f, rb2d.velocity.y);

                Invoke("NolongerInAir", .5f);
            }

        }
    }

    private void FollowPlayer()
    {
        if (canChasePlayer)
        {
            if (Mathf.Abs(transform.position.x - player.position.x) < minDistance)
                tooClose = true;
            else
                tooClose = false;

            if (tooClose)
                currentVelocity = 0f;
        }
        else
            tooClose = false;

    }

    private void NolongerInAir()
    {
        isJumping = false;

        if (standStill)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }

    private void CheckFloorEdge()
    {
        if (rayHitNumber == 1 && canTurnAroundEdge && !isWaiting)
        {
            isWaiting = true;
            currentTimer = originalWaitTime;
            isFacingLeft = !isFacingLeft;
        }
    }

    private void HandleWait()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0f)
        {
            isWaiting = false;
            currentTimer = 0;
        }
    }

    public void MovePlayer(float horizontalInput)
    {
        if (!tooClose || !standStill)
        {
            if (horizontalInput != 0)
            {
                if (horizontalInput != moveDirection)
                    currentVelocity = 0f;

                moveDirection = horizontalInput;
            }

            currentVelocity = CheckSpeed(horizontalInput);
        }
    }

    private float CheckSpeed(float horizontalInput)
    {
        if (Mathf.Abs(horizontalInput) > 0)
            currentVelocity += movementStats.acceleration * Time.deltaTime;
        else
            currentVelocity -= movementStats.deceleration * Time.deltaTime;

        return Mathf.Clamp(currentVelocity, 0, movementStats.maxSpeed);
    }

    public void AddForceVelocityY()
    {
        rb2d.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
    }
}
