using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour
{
    [field: SerializeField] private UnityEvent<float> OnMovementInput { get; set; }
    [field: SerializeField] private UnityEvent OnJumpInput { get; set; }
    [field: SerializeField] private UnityEvent OnJumpRelease { get; set; }

    private void Update()
    {
        MovemnetPressed();
        JumpPressed();
    }

    private void MovemnetPressed()
    {
        OnMovementInput?.Invoke(Input.GetAxisRaw("Horizontal"));
    }

    private void JumpPressed()
    {
        if (Input.GetButtonDown("Jump"))
            OnJumpInput?.Invoke();
        if (Input.GetButtonUp("Jump"))
            OnJumpRelease?.Invoke();
    }
}
