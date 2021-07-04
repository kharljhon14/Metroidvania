using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : Enemy
{
    [field: SerializeField] private UnityEvent<float> OnMoveInput { get; set; }

    [Header("Enemy Flags")]
    [SerializeField] protected bool isFacingLeft;
    [SerializeField] private bool canTurnAroundCollider;
    [SerializeField] private bool canTurnAroundEdge;
    [SerializeField] private bool canChasePlayer;
    [SerializeField] private bool canJump;

    [Header("Timers")]
    [SerializeField] private float originalWaitTime;

    [SerializeField] LayerMask collision;

    private float moveDirection = 1f;
    private float currentTimer;
    private bool isWaiting;

    private void Start()
    {
        currentTimer = originalWaitTime;
    }

    private void Update()
    {
        OnMoveInput?.Invoke(moveDirection);
        CheckEdge();
        Move();
        CheckFloorEdge();
        HandleWait();
    }

    private void Move()
    {
        float distance = .5f;
        if (isFacingLeft)
        {
            moveDirection = -1f;
            if (CollisionCheck(Vector2.left, distance, collision) && canTurnAroundCollider)
            {
                isFacingLeft = false;
            }
        }

        else
        {
            moveDirection = 1f;
            if (CollisionCheck(Vector2.right, distance, collision) && canTurnAroundCollider)
            {
                isFacingLeft = true;
            }
        }
    }

    private void CheckFloorEdge()
    {
        if (rayHitNumber == 1 && canTurnAroundEdge && !isWaiting)
        {
            currentTimer = originalWaitTime;
            isWaiting = true;
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
