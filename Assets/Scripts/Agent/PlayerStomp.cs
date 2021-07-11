using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStomp : MonoBehaviour
{
    private bool stomp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (stomp)
            return;

        stomp = true;
        IHitable hitable = collision.GetComponent<IHitable>();
        hitable?.GetHit(1, gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        stomp = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        stomp = false;
    }
}
