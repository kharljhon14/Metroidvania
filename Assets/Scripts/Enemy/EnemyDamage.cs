using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private bool hit = false;
    private float knockbackPower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit)
            return;

        hit = true;

        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            IHitable hitable = collision.GetComponent<IHitable>();
            IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
            hitable?.GetHit(1, gameObject);
            if (transform.position.x > collision.transform.position.x)
                knockbackPower = -30;
            else
                knockbackPower = 30;

            knockbackable?.Knockback(Vector2.right, knockbackPower, .3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hit = false;
    }
}
