using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHitable
{
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }


    [SerializeField] protected int rayHitNumber;
    [SerializeField] protected int maxHealth;

    protected Collider2D col2d;
    protected Rigidbody2D rb2d;

    private int currentHealth;
    private bool isDead = false;

    private void Awake()
    {
        col2d = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
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

    protected virtual void CheckEdge( LayerMask groundLayer)
    {
        Ray2D[] groundRays = new Ray2D[3];

        groundRays[0].origin = new Vector2(col2d.bounds.min.x, col2d.bounds.center.y);
        groundRays[1].origin = col2d.bounds.center;
        groundRays[2].origin = new Vector2(col2d.bounds.max.x, col2d.bounds.center.y);

        //groundRays[0].origin = new Vector2(transform.position.x - (transform.localScale.x * .5f), transform.position.y - .05f);
        //groundRays[1].origin = new Vector2(transform.position.x, transform.position.y - .05f);
        //groundRays[2].origin = new Vector2(transform.position.x + (transform.localScale.x * .5f), transform.position.y - .05f);

        RaycastHit2D[] hits = new RaycastHit2D[3];
        for (int i = 0; i < 3; i++)
        {
            hits[i] = Physics2D.Raycast(groundRays[i].origin, -transform.up, Mathf.Abs(transform.localScale.x * .6f), groundLayer);
            Debug.DrawRay(groundRays[i].origin, -transform.up * Mathf.Abs(transform.localScale.x * .6f), Color.blue);
        }
        int numberOfHits = 0;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit)
                numberOfHits++;
        }
        rayHitNumber = numberOfHits;
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!isDead)
        {
            Debug.Log("HIT");
            currentHealth -= damage;
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                Debug.Log("Dead");
            }
        }
    }
}
