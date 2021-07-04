using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Collider2D col2d;
    [SerializeField] protected int rayHitNumber;

    private void Awake()
    {
        col2d = GetComponent<Collider2D>();
    }

    protected virtual bool CollisionCheck(Vector2 direction, float distance, LayerMask collision)
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

    protected virtual void CheckEdge()
    {
        Ray2D[] groundRays = new Ray2D[3];

        groundRays[0].origin = new Vector2(col2d.bounds.min.x, col2d.bounds.center.y);
        groundRays[1].origin = col2d.bounds.center;
        groundRays[2].origin = new Vector2(col2d.bounds.max.x, col2d.bounds.center.y);

        RaycastHit2D[] hits = new RaycastHit2D[3];
        int numberOfHits = 0;
        for (int i = 0; i < 3; i++)
        {
            Debug.DrawRay(groundRays[i].origin, -transform.up * .5f, Color.red);
            hits[i] = Physics2D.Raycast(groundRays[i].origin, -transform.up, Mathf.Abs(transform.localScale.x * .5f));
        }

        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
            {
                numberOfHits++;
            }
            else
                numberOfHits--;
        }
        rayHitNumber = numberOfHits;
    }
}
