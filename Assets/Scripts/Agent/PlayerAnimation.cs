using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetIdleAnimation(bool isGrounded)
    {
        animator.SetBool("Grounded", isGrounded);
    }

    public void SetRunningAnimation(float currentVelocity)
    {
        animator.SetBool("Running", Mathf.Abs(currentVelocity) > 0);
    }

    public void SetJumpFallAnimation(float currentVelocity)
    {
        animator.SetFloat("VelocityY", currentVelocity);
    }

    public void SetChasingAnimation(bool isPlayerDetected)
    {
        animator.SetBool("Chasing", isPlayerDetected);
    }
}
