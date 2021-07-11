using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : Enemy
{
    [SerializeField] private enum DetectionType { Rectangle, Circle }
    [SerializeField] private DetectionType type;
    [SerializeField] private float distance;
    [SerializeField] private Vector2 detectionOffset;
    [SerializeField] protected bool followPlayerIfFound;
    [SerializeField] protected float radius;
    [SerializeField] protected LayerMask whatIsPlayer;
    private EnemyBrain enemyBrain;

    private void Start()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        RaycastHit2D hit;

        if (type == DetectionType.Rectangle)
        {
            if (!enemyBrain.isFacingLeft)
                hit = Physics2D.BoxCast(new Vector2(transform.position.x + col2d.bounds.extents.x + detectionOffset.x + (distance * .5f), col2d.bounds.center.y), new Vector2(distance, col2d.bounds.size.y + detectionOffset.y), 0, Vector2.zero, 0, whatIsPlayer);

            else
                hit = Physics2D.BoxCast(new Vector2(transform.position.x - col2d.bounds.extents.x - detectionOffset.x - (distance * .5f), col2d.bounds.center.y), new Vector2(distance, col2d.bounds.size.y + detectionOffset.y), 0, Vector2.zero, 0, whatIsPlayer);

            if (hit)
                enemyBrain.canChasePlayer = true;
            else
                enemyBrain.canChasePlayer = false;
        }

        if (type == DetectionType.Circle)
        {
            hit = Physics2D.CircleCast(transform.position, radius, Vector2.zero, 0f, whatIsPlayer);
            if (hit)
                enemyBrain.canChasePlayer = true;
            else
                enemyBrain.canChasePlayer = false;
        }

    }

    private void OnDrawGizmos()
    {
        col2d = GetComponent<Collider2D>();
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (type == DetectionType.Rectangle)
        {
            Gizmos.color = Color.red;
            if (!sr.flipX)
            {
                Gizmos.DrawWireCube(new Vector2(transform.position.x + col2d.bounds.extents.x + detectionOffset.x + (distance * .5f), col2d.bounds.center.y + detectionOffset.y), new Vector2(distance, col2d.bounds.size.y));
            }
            else
            {
                Gizmos.DrawWireCube(new Vector2(transform.position.x - col2d.bounds.extents.x - detectionOffset.x - (distance * .5f), col2d.bounds.center.y + detectionOffset.y), new Vector2(distance, col2d.bounds.size.y));
            }
        }
        if (type == DetectionType.Circle)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(col2d.bounds.center, radius);
        }
    }
}
