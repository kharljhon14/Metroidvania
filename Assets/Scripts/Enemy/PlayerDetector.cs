using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
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
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.zero, 0f, whatIsPlayer);
        if (hit)
        {
            enemyBrain.canChasePlayer = true;
        }
        else
        {
            enemyBrain.canChasePlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
