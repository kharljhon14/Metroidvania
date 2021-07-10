using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : Enemy
{
    [field: SerializeField] private UnityEvent<float> OnMoveInput { get; set; }
    [field: SerializeField] private UnityEvent OnJumpInput { get; set; }

    [Header("Enemy Flags")]
    [SerializeField] private bool spawnFacingLeft;
    [SerializeField] private bool canTurnAroundCollider;
    [SerializeField] private bool canTurnAroundEdge;
    [SerializeField] private bool canChasePlayer;
    [SerializeField] private bool canJump;

    [Header("Timers")]
    [SerializeField] private float originalWaitTime;
    [SerializeField] private float originalTimeTillDoAction;

    [SerializeField] LayerMask collision;
    [SerializeField] private float minDistance;

    protected bool isFacingLeft;
    protected Transform player;

    private float moveDirection = 1f;
    private float currentTimer;
    private float timeTillDoAction;
    private bool isWaiting;
    private bool isJumping;

    private void Start()
    {
        if (spawnFacingLeft)
            isFacingLeft = true;

        player = FindObjectOfType<Character>().transform;
        currentTimer = originalWaitTime;
        timeTillDoAction = originalTimeTillDoAction;
    }

    private void Update()
    {
        OnMoveInput?.Invoke(moveDirection);
        CheckFloorEdge();
        CheckEdge(collision);
        HandleWait();
        FollowPlayer();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
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
                timeTillDoAction = originalTimeTillDoAction;
                Invoke("NolongerInAir", .5f);

                rb2d.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            }
        }
    }

    private void FollowPlayer()
    {
        if (canChasePlayer)
        {
            bool tooClose = new bool();
            if (Mathf.Abs(transform.position.x - player.position.x) < minDistance)
                tooClose = true;
            else
                tooClose = false;

            if (tooClose)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
    }

    private void NolongerInAir()
    {
        isJumping = false;
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
}
