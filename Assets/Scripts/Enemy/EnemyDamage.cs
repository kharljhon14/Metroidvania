using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private bool hit = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit)
            return;

        hit = true;

        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            IHitable hitable = collision.GetComponent<IHitable>();
            hitable?.GetHit(1, gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hit = false;
    }
}
