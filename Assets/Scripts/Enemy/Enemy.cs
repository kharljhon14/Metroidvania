using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [field: SerializeField] private UnityEvent<float> OnMoveInput { get; set; }

    [SerializeField] LayerMask collision;
    private Collider2D col2d;

    private float moveDirection = 1f;

    private void Awake()
    {
        col2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        OnMoveInput?.Invoke(moveDirection);
        Move();
    }

    private bool CollisionCheck(Vector2 direction, float distance, LayerMask collision)
    {
        RaycastHit2D[] raycastHit2D = new RaycastHit2D[10];
        int numHits = col2d.Cast(direction, raycastHit2D, distance);


        for (int i = 0; i < numHits; i++)
        {
            if ((1 << raycastHit2D[i].collider.gameObject.layer & collision) != 0)
                return true;
        }
        return false;
    }

    private void Move()
    {
        if (CollisionCheck(Vector2.right, .5f, collision))
        {
            moveDirection = -1f;
        }

        if (CollisionCheck(Vector2.left, .5f, collision))
        {
            moveDirection = 1f;
        }
    }
}
